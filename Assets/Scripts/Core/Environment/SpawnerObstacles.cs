using System.Collections.Generic;
using Core.Environment;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerObstacles : MonoBehaviour
{
    [SerializeField] private List<Obstacle> _obstacles;
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
        var obstacles = Instantiate(_obstacles[Random.Range(0, _obstacles.Count)],
            RandomPositionFinder.FindRandomPosition(transform, _player, -41, 15, -17, 17).position,
            Quaternion.Euler(0, Random.Range(0, 360), 0), _parent);
        _obstacles.Add(obstacles);
    }

    public void ChangePositionObstacle(Obstacle obstacle)
    {
        obstacle.transform.position =
            RandomPositionFinder.FindRandomPosition(transform, _player, -41, 15, -17, 17).position;
    }
}