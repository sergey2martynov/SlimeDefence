using System.Collections.Generic;
using UnityEngine;

namespace StaticData.Player
{
    [CreateAssetMenu(fileName = "PlayerLevelStages", menuName = "StaticData/PlayerLevelStages", order = 51)]

    public class PlayerLevelStages : ScriptableObject
    {
        [SerializeField] private List<int> _experience;

        public List<int> Experience => _experience;
    }
}
