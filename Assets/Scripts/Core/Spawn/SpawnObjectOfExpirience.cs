using UnityEngine;

public class SpawnObjectOfExpirience : MonoBehaviour
{
    [SerializeField] private ObjectOfExperiencePool _pool;

    public void CreateObjOfExperience(Transform transformEnemy, EnemyType enemyType)
    {
        GameObject objectOfExperience;
        
        if (enemyType == EnemyType.Weak && Random.Range(0, 10) < 1)
        {
            objectOfExperience = _pool.Pool.Get();
            objectOfExperience.transform.position = transformEnemy.position;
        }

        if (enemyType == EnemyType.Average && Random.Range(0, 10) < 3)
        {
            objectOfExperience = _pool.Pool.Get();
            objectOfExperience.transform.position = transformEnemy.position;
        }

        if (enemyType == EnemyType.Strong && Random.Range(0, 10) < 6)
        {
            objectOfExperience = _pool.Pool.Get();
            objectOfExperience.transform.position = transformEnemy.position;
        }
    }
}