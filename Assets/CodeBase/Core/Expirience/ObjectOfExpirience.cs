using CodeBase.Core;
using CodeBase.Core.Character.Player;
using UnityEngine;

public class ObjectOfExpirience : MonoBehaviour, IPoolObject
{
    [SerializeField] private int _expirience;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerControler playerController))
        {
            playerController.GetExperience(_expirience);
        }
    }
}
