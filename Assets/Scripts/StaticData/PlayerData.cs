using UnityEngine;

namespace StaticData
{
    public static class PlayerData
    {
        public static int Level
        {
            get => PlayerPrefs.GetInt("Level_Player");
            set => PlayerPrefs.SetInt("Level_Player", value);
        }
        
        public static int Experience
        {
            get => PlayerPrefs.GetInt("Experience_Player");
            set => PlayerPrefs.SetInt("Experience_Player", value);
        }
    }
}
