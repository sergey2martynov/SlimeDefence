using TMPro;
using UnityEngine;

public class KillCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _counterText;

    private int _counter;

    public void IncreaseCounter()
    {
        _counter++;
        _counterText.text = _counter.ToString();
    }
}
