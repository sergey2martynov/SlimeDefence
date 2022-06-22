using System.Collections.Generic;
using UnityEngine;

namespace Analytics
{
    public static class EventSender
    {
        private static float level_start_time;
        private static float level_finish_time;

        public static void SendLevelStart()
        {
            var metrica = AppMetrica.Instance;
            level_start_time = Time.time;

            Debug.Log("level_start");

            var parametrs = new Dictionary<string, object>
            {
                {"level_number", ProgressData.CurrentWave + 1},
                {"level_name", "Wave_" + (ProgressData.CurrentWave + 1)},
                {"level_count", ProgressData.LevelCount + 1}
            };

            metrica.ReportEvent("level_start", parametrs);
            metrica.SendEventsBuffer();
        }

        public static void SendLevelFinish(string levelType = "normal")
        {
            float time = Time.time - level_start_time;
            Debug.Log("level_finish");

            var metrica = AppMetrica.Instance;
            var parametrs = new Dictionary<string, object>
            {
                {"level_number", ProgressData.CurrentWave + 1},
                {"level_name", "Wave_" + (ProgressData.CurrentWave + 1)},
                {"level_count", ProgressData.LevelCount + 1},
                {"time", Mathf.RoundToInt(time)},
            };
            
            Debug.Log("time" + parametrs["time"]);

            metrica.ReportEvent("level_finish", parametrs);
            metrica.SendEventsBuffer();
        }
    }
}