using UnityEngine;

namespace Core.Environment
{
    public class SpawnerHealthBox : MonoBehaviour
    {
        [SerializeField] private HealthBox _healthBox;
        
        private HealthBox _spawnedHealthBox;

        private void Start()
        {
            SpawnHealthBox();
        }

        public void SpawnHealthBox()
        {
            _spawnedHealthBox = Instantiate(_healthBox,new Vector3(0,-10, 0) ,
                Quaternion.Euler(0, Random.Range(0, 360), 0));
        }

        public void ChangePositionHealthBox(Transform newTransform)
        {
            _spawnedHealthBox.gameObject.SetActive(true);
            _spawnedHealthBox.transform.position = newTransform.position;
        }
    }
}