using Assets.ProjectFiles.Scripts.Enums;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    private AudioSource _audioSource;
    private Dictionary<Sounds, AudioClip> _loadedSounds;
    private float _volume = 0.6f;

    private void Awake()
    {
        #region Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        #endregion


        _audioSource = GetComponent<AudioSource>();

        _loadedSounds = new Dictionary<Sounds, AudioClip>();
        _volume = PlayerPrefs.GetFloat("soundVolume", 0.6f);

        foreach (Sounds sound in Enum.GetValues(typeof(Sounds)))
        {
            _loadedSounds.Add(sound, Resources.Load<AudioClip>(sound.ToString()));
        }
    }

    public void PlaySound(Sounds sound)
    {
        _loadedSounds.TryGetValue(sound, out var audioClip);
        if (audioClip != null)
        {
            _audioSource.PlayOneShot(audioClip, _volume);
        }
    }

    public void IncreaseVolume()
    {
        _volume += .2f;
        _volume = Mathf.Clamp01(_volume);
        PlayerPrefs.SetFloat("soundVolume", _volume);
    }

    public void DecreaseVolume()
    {
        _volume -= .2f;
        _volume = Mathf.Clamp01(_volume);
        PlayerPrefs.SetFloat("soundVolume", _volume);
    }
}
