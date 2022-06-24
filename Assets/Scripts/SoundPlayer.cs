using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _buttonSound;

    public void ButtonSoundPlay()
    {
        _buttonSound.Play();
    }
}
