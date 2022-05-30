using UnityEngine;

public class SpawnObjectOfExperience : MonoBehaviour
{
    [SerializeField] private ObjectOfExperiencePool _pool;

    public void SpawnObjOfExperienceForEnemy(Transform transformEnemy, EnemyType enemyType)
    {
        if (enemyType == EnemyType.Weak && Random.Range(0, 10) < 2)
        {
            SpawnObjOfExperience(transformEnemy);
        }

        if (enemyType == EnemyType.Average && Random.Range(0, 10) < 3)
        {
            SpawnObjOfExperience(transformEnemy);
        }

        if (enemyType == EnemyType.Strong && Random.Range(0, 10) < 5)
        {
            SpawnObjOfExperience(transformEnemy);
        }
    }

    public void SpawnObjOfExperience(Transform transform)
    {
        GameObject objectOfExperience;
        
        objectOfExperience = _pool.Pool.Get();
        objectOfExperience.transform.position = transform.position;
    }
}