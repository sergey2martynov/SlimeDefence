using Core.Character.Player;
using UnityEngine;

public class PikUpRadius : MonoBehaviour
{
   [SerializeField] private PlayerController _playerController;

   public PlayerController Controller => _playerController;
}
