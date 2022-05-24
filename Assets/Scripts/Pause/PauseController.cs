using UnityEngine;

public class PauseController : MonoBehaviour
{
    public void Pause(bool isPause)
    {
        if (isPause)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
}
