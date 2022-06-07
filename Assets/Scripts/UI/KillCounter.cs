using TMPro;
using UnityEngine;

public class KillCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _counterText;

    private int _killCounter;

    public int Counter => _killCounter;

    public void IncreaseCounter()
    {
        _killCounter++;
        _counterText.text = _killCounter.ToString();
    }
}
