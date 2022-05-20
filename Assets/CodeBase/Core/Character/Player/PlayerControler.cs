using UnityEngine;

namespace CodeBase.Core.Character.Player
{
    public class PlayerControler : MonoBehaviour
    {
        private int _expirience;

        public int Expirience => _expirience;

        public void GetExperience(int experience)
        {
            _expirience += experience;
        }
    }
}
