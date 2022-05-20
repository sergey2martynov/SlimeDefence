using UnityEngine;

public class SpawnObjectOfExpirience : MonoBehaviour
{
    [SerializeField] private ObjectOfExpirience _object;

    public void CreateObjOfExperience(Transform transformEnemy, EnemyType enemyType)
    {
        if (enemyType == EnemyType.Weak && Random.Range(0, 10) < 2)
            Instantiate(_object, transformEnemy.position, Quaternion.identity);

        if (enemyType == EnemyType.Average && Random.Range(0, 10) < 5)
            Instantiate(_object, transformEnemy.position, Quaternion.identity);

        if (enemyType == EnemyType.Strong && Random.Range(0, 10) < 8)
            Instantiate(_object, transformEnemy.position, Quaternion.identity);
    }
}