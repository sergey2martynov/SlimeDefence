using UnityEngine;

namespace Core.Environment.Chest
{
    public class Chest : MonoBehaviour
    {
        private SpawnerСhest _spawnerСhest;
        private SpawnObjectOfExperience _spawnObjectOfExperience;
        private SpawnerHealthBox _spawnerHealthBox;

        public void Initialize(SpawnObjectOfExperience spawnObjectOfExperience, SpawnerСhest spawnerСhest, SpawnerHealthBox spawnerHealtBox)
        {
            _spawnObjectOfExperience = spawnObjectOfExperience;
            _spawnerСhest = spawnerСhest;
            _spawnerHealthBox = spawnerHealtBox;
        }
    
        public void DestroyChest()
        {
            if(Random.Range(0,2) == 0)
                _spawnObjectOfExperience.SpawnObjOfExperience(transform);
            else
                _spawnerHealthBox.ChangePositionHealthBox(transform);
        
            _spawnerСhest.ChangePositionChest(this);
        
        }
    }
}
