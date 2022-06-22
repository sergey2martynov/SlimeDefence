using UnityEngine;

public static class ProgressData
{
    public static int CurrentWave;
    
    public static int LevelCount
    {
        get => PlayerPrefs.GetInt("Level_Number");
        set => PlayerPrefs.SetInt("Level_Number", value);
    }
}