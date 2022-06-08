using Core.Character.Player;
using UnityEngine;

namespace Core.Environment
{
    public class HealthBox : MonoBehaviour
    {
        [SerializeField] private Transform _parent;

        public Transform Parent => _parent;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Player playerController))
            {
                playerController.Health.SetHealthPoint();
                Destroy(gameObject);
            }
        }
    }
}
