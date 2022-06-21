using Core.Expirience;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Environment.Chest
{
    public class Chest : MonoBehaviour
    {
        [SerializeField] private Experience _experience;
        private ExperiencePool _pool;
        private Transform _player;
        private SpawnerСhest _spawnerСhest;
        private SpawnerHealthBox _spawnerHealthBox;

        public void Initialize(SpawnerСhest spawnerСhest, SpawnerHealthBox spawnerHealtBox, ExperiencePool pool, Transform player)
        {
            _spawnerСhest = spawnerСhest;
            _spawnerHealthBox = spawnerHealtBox;
            _pool = pool;
            _player = player;
        }
    
        public void DestroyChest()
        {
            if(Random.Range(0,2) == 0)
                _spawnerHealthBox.ChangePositionHealthBox(transform);
            else
            {
                var experience = Instantiate(_experience, transform.position, Quaternion.identity);
                
                experience.Initialize(_pool, _player);
            }
            _spawnerСhest.ChangePositionChest(this);
        }
    }
}
