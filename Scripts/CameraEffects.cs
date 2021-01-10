using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering;

public class CameraEffects : MonoBehaviour
{
    public static CameraEffects Instance { get; private set; }

    [SerializeField] Transform chromaticAbarattionEffectObj;

    private float _timer;
    private float _timerMax;
    private CinemachineBasicMultiChannelPerlin _cinemachineMultiChannelPerlin;
    private Volume _caVolume;
    private float _decreaseSpeed;
    private float _startingIntencity;

    private void Awake()
    {
        #region Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        #endregion

        var vCam = GetComponent<CinemachineVirtualCamera>();
        _cinemachineMultiChannelPerlin = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _caVolume = chromaticAbarattionEffectObj.GetComponent<Volume>();
        _decreaseSpeed = 1f;
    }

    private void Update()
    {
        if (_timer < _timerMax)
        {
            _timer += Time.deltaTime;
            var amplitude = Mathf.Lerp(_startingIntencity, 0f, _timer / _timerMax);
            _cinemachineMultiChannelPerlin.m_FrequencyGain = amplitude;
        }

        if (_caVolume.weight > 0)
        {
            _caVolume.weight -= Time.deltaTime * _decreaseSpeed;
        }
    }

    public void SetVolumeWeight(float weight)
    {
        _caVolume.weight = weight;
    }

    public void ShakeCamera(float intencity, float timerMax)
    {
        _timerMax = timerMax;
        _timer = 0f;
        _startingIntencity = intencity;
        _cinemachineMultiChannelPerlin.m_FrequencyGain = intencity;
    }
}
