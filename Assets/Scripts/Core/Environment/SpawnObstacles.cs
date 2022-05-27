using System.Collections.Generic;
using Core.Environment;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnObstacles : MonoBehaviour
{
    [SerializeField] private List<GameObject> _obstacles;
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _parent;
    [SerializeField] private int _maxNumberOfObstacles;

    private int _spawnCount;

    private void Start()
    {
        for (int i = 0; i < _maxNumberOfObstacles; i++)
        {
            SpawnObstacle();
        }
    }

    private void Update()
    {
        if (_obstacles.Count < _maxNumberOfObstacles)
        {
            SpawnObstacle();
        }
    }

    public void SpawnObstacle()
    {
        var obstacles = Instantiate(_obstacles[Random.Range(0, _obstacles.Count)], FindRandomPosition().position,
            Quaternion.Euler(0, Random.Range(0,360), 0), _parent);
        _obstacles.Add(obstacles);
    }

    public void ChangePositionObstacle(Obstacle obstacle)
    {
        obstacle.transform.position = FindRandomPosition().position;
    }
    
    public Transform FindRandomPosition()
    {
        Transform vector = transform;
        
        if (_spawnCount == 0)
        {
            vector.position = new Vector3(
                0,
                0,
                Random.Range(0, 2) == 0 ? Random.Range(-42f, -40f) : Random.Range(14f, 16f));
            
            _spawnCount++;
        }
        else if (_spawnCount == 1)
        {
            vector.position = new Vector3(
                Random.Range(0, 2) == 0 ? Random.Range(16f, 18f) : Random.Range(-18f, -16f),
                0,
                0);
            
            _spawnCount++;
        }
        else
        {
            vector.position = new Vector3(
                Random.Range(0, 2) == 0 ? Random.Range(18f, 20f) : Random.Range(-20f, -18f),
                0,
                Random.Range(0, 2) == 0 ? Random.Range(-42f, -40f) : Random.Range(14f, 16f));

            _spawnCount = 0;
        }

        vector.position += _player.position;

        return vector;
    }
}
