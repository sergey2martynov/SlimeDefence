using UnityEngine;

public class DontDestoyOnLoad : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}