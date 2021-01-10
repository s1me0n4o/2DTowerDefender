using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource _audioSource;
    private float _volume = 0.6f;

    private void Awake()
    {
        _volume = PlayerPrefs.GetFloat("musicVolume", 0.6f);
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = _volume;
    }

    public void IncreaseVolume()
    {
        _volume += .2f;
        _volume = Mathf.Clamp01(_volume);
        _audioSource.volume = _volume;
        PlayerPrefs.SetFloat("musicVolume", _volume);
    }

    public void DecreaseVolume()
    {
        _volume -= .2f;
        _volume = Mathf.Clamp01(_volume);
        _audioSource.volume = _volume;
        PlayerPrefs.SetFloat("musicVolume", _volume);
    }

}
