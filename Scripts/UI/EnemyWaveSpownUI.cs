using UnityEngine;
using TMPro;
using System;
using Assets.ProjectFiles.Scripts;

public class EnemyWaveSpownUI : MonoBehaviour
{
    private TextMeshProUGUI _nextWaveInfoText;
    private TextMeshProUGUI _waveText;
    private RectTransform _arrowUI;
    private float _remainingTimeToNextWave;
    private Camera _camera;
    private bool _isInitialStart;

    private void Awake()
    {
        _nextWaveInfoText = transform.Find("NextWaveInfoText").GetComponent<TextMeshProUGUI>();
        _waveText = transform.Find("WaveText").GetComponent<TextMeshProUGUI>();
        _arrowUI = transform.Find("IncomingEnemyArrow").GetComponent<RectTransform>();
    }

    private void Start()
    {
        _camera = Camera.main;
        _isInitialStart = true;
        _remainingTimeToNextWave = 0;
        Hide();

        EnemyWaveManager.Instance.OnEnemySpown += EnemyWaveManager_OnEnemySpown;
        EnemyWaveManager.Instance.OnWaveIndexChange += EnemyWaveManager_OnWaveIndexChange;
    }

    private void EnemyWaveManager_OnWaveIndexChange(int waveIndex)
    {
        if (_isInitialStart)
        {
            Show();
            _isInitialStart = false;
        }

        SetWaveInfo($"Wave: {waveIndex}");
    }

    private void Update()
    {
        if (_remainingTimeToNextWave >= 0)
        {
            _remainingTimeToNextWave -= Time.deltaTime;
            SetNextWaveInfo($"Next wave spown in \n{_remainingTimeToNextWave.ToString("F1")} seconds");
        }

        var dirToNextSpownPos = (EnemyWaveManager.Instance.GetNextSpownPos() - _camera.transform.position).normalized;
        _arrowUI.anchoredPosition = dirToNextSpownPos * 300f;
        _arrowUI.transform.eulerAngles = new Vector3(0, 0, Utils.GetAngleFromVector(dirToNextSpownPos));

        var distanceToNextSpownPos = Vector3.Distance(EnemyWaveManager.Instance.GetNextSpownPos(), _camera.transform.position);
        _arrowUI.gameObject.SetActive(distanceToNextSpownPos > _camera.orthographicSize * 1.5f);
    }

    private void EnemyWaveManager_OnEnemySpown(float respownTime)
    {
        _remainingTimeToNextWave = respownTime;
    }

    private void SetWaveInfo(string text)
    {
        _waveText.SetText(text);
    }

    private void SetNextWaveInfo(string text)
    {
        _nextWaveInfoText.SetText(text);
    }

    private void Show()
    {
        _arrowUI.gameObject.SetActive(true);
        _waveText.gameObject.SetActive(true);
        _nextWaveInfoText.gameObject.SetActive(true);
    }

    private void Hide()
    {
        _arrowUI.gameObject.SetActive(false);
        _waveText.gameObject.SetActive(false);
        _nextWaveInfoText.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        EnemyWaveManager.Instance.OnEnemySpown -= EnemyWaveManager_OnEnemySpown;
        EnemyWaveManager.Instance.OnWaveIndexChange -= EnemyWaveManager_OnWaveIndexChange;
    }
}
