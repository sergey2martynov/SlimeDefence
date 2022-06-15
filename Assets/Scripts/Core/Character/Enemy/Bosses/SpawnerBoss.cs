using CodeBase.Core.Character.Enemy;
using Core.Environment;
using UnityEngine;

public class SpawnerBoss : MonoBehaviour
{
    [SerializeField] private Enemy _finalBoss;
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _parent;
    [SerializeField] private KillCounter _killCounter;
    [SerializeField] private TimeCounter _timeCounter;
    [SerializeField] private ExperiencePool _experiencePool;
    [SerializeField] private Camera _camera;
    [SerializeField] private BloodSplatPool _bloodSplat;
    public int SpawnedBosses { get; set;}
    
    private void Start()
    {
        _timeCounter.SpawnBossTimeHasCome += SpawnFinalBoss;
    }

    private void OnDestroy()
    {
        _timeCounter.SpawnBossTimeHasCome -= SpawnFinalBoss;
    }

    private void SpawnBoss(Enemy enemy)
    {
        var finalBoss = Instantiate(enemy, _player.position + FindSpawnRandomPosition(), Quaternion.identity, _parent);
        finalBoss.Initialize(_killCounter, _experiencePool, _camera, this, _timeCounter, _bloodSplat);
    }

    private void SpawnFinalBoss(int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnBoss(_finalBoss);
            SpawnedBosses++;
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