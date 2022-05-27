using Core.Environment;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private SpawnerСhest _spawnerСhest;
    private SpawnObjectOfExperience _spawnObjectOfExperience;
    private SpawnerHealthBox _spawnerHealtBox;

    public void Initialize(SpawnObjectOfExperience spawnObjectOfExperience, SpawnerСhest spawnerСhest, SpawnerHealthBox spawnerHealtBox)
    {
        _spawnObjectOfExperience = spawnObjectOfExperience;
        _spawnerСhest = spawnerСhest;
        _spawnerHealtBox = spawnerHealtBox;
    }
    
    public void DestroyChest()
    {
        if(Random.Range(0,2) == 0)
            _spawnObjectOfExperience.SpawnObjOfExperience(transform);
        else
            _spawnerHealtBox.ChangePositionHealthBox(transform);
        
        _spawnerСhest.ChangePositionChest(this);
        
    }
}
