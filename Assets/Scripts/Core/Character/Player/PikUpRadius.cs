using Core.Character.Player;
using UnityEngine;
using UnityEngine.Serialization;

public class PikUpRadius : MonoBehaviour
{
   [SerializeField] private Player _player;

   public Player Controller => _player;
}
