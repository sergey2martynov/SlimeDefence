using UnityEngine;

namespace CodeBase.Core.Character.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private ProgressController _progressController;

        public ProgressController ProgressController => _progressController;
    }
}
