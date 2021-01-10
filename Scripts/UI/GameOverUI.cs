using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.ProjectFiles.Scripts;
using Assets.ProjectFiles.Scripts.Enums;

public class GameOverUI : MonoBehaviour
{
    private Transform _panel;
    private Button _retryBtn;
    private Button _homeBtn;
    private TextMeshProUGUI _textSurvivedWaves;

    private void Awake()
    {
        _panel = transform.Find("panel");
        _retryBtn = _panel.Find("retryBtn").GetComponent<Button>();
        _homeBtn = _panel.Find("homeBtn").GetComponent<Button>();
        _textSurvivedWaves = _panel.Find("textSurvivedWaves").GetComponent<TextMeshProUGUI>();

        _retryBtn.onClick.AddListener(() => { GameSceneManager.LoadScene(GameScene.MainScene); });
        _homeBtn.onClick.AddListener(() => { GameSceneManager.LoadScene(GameScene.MenuScene); });

        HidePanel();
    }

    public void ShowPanel()
    {
        _panel.gameObject.SetActive(true);
        _textSurvivedWaves.SetText($"YOU SURVIVED { EnemyWaveManager.Instance.GetWaveIndex()} WAVES!");
    }

    private void HidePanel()
    {
        _panel.gameObject.SetActive(false);
    }
}
