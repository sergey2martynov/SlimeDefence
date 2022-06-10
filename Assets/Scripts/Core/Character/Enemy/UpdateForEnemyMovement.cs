using System;
using System.Collections;
using System.Threading;
using CodeBase.Core.Character.Enemy;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class UpdateForEnemyMovement : MonoBehaviour
{
    [SerializeField] private SpawnerEnemies _spawnerEnemies;
    [SerializeField] private bool _isCanMove ;

    // private void Start()
    // {
    //     var t1 = new Thread(() => StartCoroutine(IEUpdate(0, _spawnerEnemies.SpawnedEnemies.Count / 4)));
    //     var t2 = new Thread(() => StartCoroutine(IEUpdate(_spawnerEnemies.SpawnedEnemies.Count / 4 + 1 , _spawnerEnemies.SpawnedEnemies.Count / 2)));
    //     var t3 = new Thread(() => StartCoroutine(IEUpdate(_spawnerEnemies.SpawnedEnemies.Count / 2 + 1 , _spawnerEnemies.SpawnedEnemies.Count * 3 / 4)));
    //     var t4 = new Thread(() => StartCoroutine(IEUpdate(_spawnerEnemies.SpawnedEnemies.Count * 3 / 4 + 1, _spawnerEnemies.SpawnedEnemies.Count)));
    //     
    //     t1.Start();
    //     t2.Start();
    //     t3.Start();
    //     t4.Start();
    // }
    //
    // private IEnumerator IEUpdate(int startIndex, int lastIndex)
    // {
    //     while (true)
    //     {
    //         for (int i = startIndex; i < lastIndex; i++)
    //         {
    //             _spawnerEnemies.SpawnedEnemies[i].Movement.Move();
    //         }
    //
    //         yield return new WaitForFixedUpdate();
    //     }
    // }

    private void FixedUpdate()
    {
        for (int i = 0; i < _spawnerEnemies.SpawnedEnemies.Count; i++)
        {
            _spawnerEnemies.SpawnedEnemies[i].Movement.Move();
        }
    }
    //
    // private struct MoveJob : Ijob
    // {
    //     public void Execute(int i)
    //     {
    //         _spawnerEnemies.SpawnedEnemies[i].Movement.Move();
    //     }
    // }
}
