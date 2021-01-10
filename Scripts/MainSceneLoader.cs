using Assets.ProjectFiles.Scripts;
using Assets.ProjectFiles.Scripts.Enums;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneLoader : MonoBehaviour
{
    private void Awake()
    {
        var panel = transform.Find("panel");
        var playBtn = panel.Find("playBtn").GetComponent<Button>();
        playBtn.onClick.AddListener(() => { LoadMainScene(); });
    }

    public void LoadMainScene()
    {
        GameSceneManager.LoadScene(GameScene.MainScene);
    }
}
