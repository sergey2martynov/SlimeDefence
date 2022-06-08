using CodeBase.Core.Character.Enemy;
using Core.Environment;
using UnityEngine;

public class SpawnerBoss : MonoBehaviour
{
    [SerializeField] private Enemy _miniBoss;
    [SerializeField] private Enemy _finalBoss;
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _parent;
    [SerializeField] private KillCounter _killCounter;
    [SerializeField] private TimeCounter _timeCounter;
    [SerializeField] private ExperiencePool _experiencePool;
    [SerializeField] private HealthBox _healthBox;
    [SerializeField] private Transform _healthBoxParent;
    [SerializeField] private WinScreen _winScreen;

    private int _currentIndex;
    private Vector3 _offsetPositionMiniBoss;
    private Vector3 _offsetPositionFinalBoss;
    
    private void Start()
    {
        _offsetPositionMiniBoss = new Vector3(0, 0, -40);
        _offsetPositionFinalBoss = new Vector3(0, 0, -20);
        _timeCounter.FinalStageBegun += SpawnFinalBoss;
        _timeCounter.SpawnMiniBossTimeHasCome += SpawnMiniBoss;
    }

    private void OnDestroy()
    {
        _timeCounter.FinalStageBegun -= SpawnFinalBoss;
        _timeCounter.SpawnMiniBossTimeHasCome -= SpawnMiniBoss;
    }

    private void SpawnBoss(Enemy enemy, Vector3 offset)
    {
        var finalBoss = Instantiate(enemy, _player.position + offset, Quaternion.identity, _parent);
        finalBoss.Initialize(_killCounter, _winScreen, _experiencePool, _healthBox, _healthBoxParent);
    }

    private void SpawnFinalBoss()
    {
        SpawnBoss(_finalBoss, _offsetPositionFinalBoss);
    }
    
    private void SpawnMiniBoss()
    {
        SpawnBoss(_miniBoss, _offsetPositionMiniBoss);
    }
}