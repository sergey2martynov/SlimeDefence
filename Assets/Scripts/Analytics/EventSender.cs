using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using StaticData.Stages;

namespace Analytics
{
    public static class EventSender
    {
        public static readonly UnityAction LevelStart = SendLevelStart;
        private static float level_start_time;

        public static bool LevelStartSent
        {
            get => PlayerPrefs.GetInt(nameof(LevelStartSent)) == 1;
            private set => PlayerPrefs.SetInt(nameof(LevelStartSent), value ? 1 : 0);
        }

        private static void SendLevelStart()
        {
            var metrica = AppMetrica.Instance;
            level_start_time = Time.time;
            Debug.Log($"StartTime{Mathf.RoundToInt(level_start_time)}");
            var activeScene = SceneManager.GetActiveScene();
            
            var parametrs = new Dictionary<string, object>
            {
                {"level_number", activeScene.buildIndex-1},
                {"level_name", activeScene.name},
                {"level_count", ProgressData.LevelCount}
            };
            
            metrica.ReportEvent("level_start", parametrs);
            metrica.SendEventsBuffer();
            LevelStartSent = true;
        }

        public static void SendLevelFinish(string levelType = "normal")
        {
            if (!LevelStartSent)
            {
                return;
            }

            float time = Time.time - level_start_time;
            Debug.Log($"Time since level load:{Mathf.RoundToInt(Time.timeSinceLevelLoad)}");
            Debug.Log($"Level duration{Mathf.RoundToInt(time)}");
            TimeOnLocation.Time = time;
            var metrica = AppMetrica.Instance;
            var activeScene = SceneManager.GetActiveScene();
            var parametrs = new Dictionary<string, object>
            {
                //{"level_number", activeScene.buildIndex-1},
                {"level_name", activeScene.name},
                {"level_count", GlobalData.LevelCount},
                //{"level_type",levelType},
                {"time", Mathf.RoundToInt(time)},
            };
            metrica.ReportEvent("level_finish", parametrs);
            metrica.SendEventsBuffer();
            GlobalData.IncreaseLevelCount();
            StagesCounts.Clear();
            stage_count_all = 1;
            LevelStartSent = false;
        }
    }
}