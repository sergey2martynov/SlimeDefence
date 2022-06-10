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
    [SerializeField] private Camera _camera;

    private int _currentIndex;
    private Vector3 _offsetPositionMiniBoss;
    private Vector3 _offsetPositionFinalBoss;
    
    private void Start()
    {
        _offsetPositionMiniBoss = new Vector3(0, 0, -40);
        _offsetPositionFinalBoss = new Vector3(0, 0, -20);
        _timeCounter.SpawnBossTimeHasCome += SpawnFinalBoss;
    }

    private void OnDestroy()
    {
        _timeCounter.SpawnBossTimeHasCome -= SpawnFinalBoss;
    }

    private void SpawnBoss(Enemy enemy, Vector3 offset)
    {
        var finalBoss = Instantiate(enemy, _player.position + FindSpawnRandomPosition(), Quaternion.identity, _parent);
        finalBoss.Initialize(_killCounter, _winScreen, _experiencePool, _healthBox, _healthBoxParent, _camera);
    }

    private void SpawnFinalBoss(int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnBoss(_finalBoss, _offsetPositionMiniBoss);
        }
    }
    
    private Vector3 FindSpawnRandomPosition()
    {
        Vector3 vector = new Vector3(
            Random.Range(-15, 15),
            0,
            Random.Range(0, 2) == 0 ? Random.Range(-60, -50) : Random.Range(50, 60));

        Vector3 turnedVector = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up) * vector;
        return vector;
    }
}