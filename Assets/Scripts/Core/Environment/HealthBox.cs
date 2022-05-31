using Core.Character.Player;
using UnityEngine;

namespace Core.Environment
{
    public class HealthBox : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Player playerController))
            {
                playerController.Health.SetHealthPoint();
                gameObject.SetActive(false);
            }
        }
    }
}
