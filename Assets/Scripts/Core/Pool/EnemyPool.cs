using CodeBase.Core;
using CodeBase.Core.Character.Enemy;
using UnityEngine;

public class EnemyPool : AbstractPool
{
    [SerializeField] private SpawnerEnemies _spawnerEnemies;
    [SerializeField] private AudioSource _deathSound;
    [SerializeField] private float _lowPitch = 0.8f;
    [SerializeField] private float _highPitch = 1.2f;
    [SerializeField] private AudioClip _sound;

    public override void Release(GameObject poolObject)
    {
        for (int i = 0; i < _pool.Count; i++)
        {
            if (poolObject == _pool[i])
            {
                _deathSound.pitch = Random.Range(_lowPitch, _highPitch);
                _deathSound.PlayOneShot(_sound);
                poolObject.GetComponent<Health>().ReturnHealthPoint();
                _spawnerEnemies.SpawnedEnemies.Remove(poolObject.GetComponent<Enemy>());
                poolObject.SetActive(false);
                return;
            }
        }
    }
}