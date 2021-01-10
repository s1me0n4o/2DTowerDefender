using Assets.ProjectFiles.Scripts;
using Assets.ProjectFiles.Scripts.Enums;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuUI : MonoBehaviour
{
    [SerializeField] private MusicManager musicManager;

    private Button[] _musicBars;
    private Button[] _soundBars;
    private Transform _panel;
    private Transform _background;
    private Transform _optionsPanelBtn;
    private int _soundCounter;
    private int _musicCounter;

    private void Awake()
    {
        _optionsPanelBtn = transform.Find("optionsPanelBtn");
        _panel = transform.Find("panel");
        _background = _panel.Find("background");

        _background.Find("homeBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            GameSceneManager.LoadScene(GameScene.MenuScene);
        });

        _optionsPanelBtn.GetComponent<Button>().onClick.AddListener(() =>
       {
           ShowOptionsPanel();
       });

        _background.Find("xBtn").GetComponent<Button>().onClick.AddListener(() => 
        {
            HideOptionsPanel();        
        });
        _musicBars = _background.Find("musicBars").GetComponentsInChildren<Button>();
        _soundBars = _background.Find("soundBars").GetComponentsInChildren<Button>();

        _soundCounter = PlayerPrefs.GetInt("soundCounter", 2);
        _musicCounter = PlayerPrefs.GetInt("musicCounter", 2);

        HandleStartupBars();
        HandleMusicButtons();
        HandleSoundButtons();
    }

    private void Start()
    {
        _panel.gameObject.SetActive(false);
        _optionsPanelBtn.gameObject.SetActive(false);
    }

    public void ShowOptionsBtn()
    {
        _optionsPanelBtn.gameObject.SetActive(true);
    }


    private void HideOptionsPanel()
    {
        _panel.gameObject.SetActive(false);
        _optionsPanelBtn.gameObject.SetActive(true);
        Time.timeScale = 1f;
    }
    private void ShowOptionsPanel()
    {
        _panel.gameObject.SetActive(true);
        _optionsPanelBtn.gameObject.SetActive(false);
        Time.timeScale = 0f;
    }

    private void HandleSoundButtons()
    {
        _background.Find("soundPlusBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            _soundCounter++;
            _soundCounter = Mathf.Clamp(_soundCounter, 0, 4);
            _soundBars[_soundCounter].interactable = true;
            SoundManager.Instance.IncreaseVolume();
            PlayerPrefs.SetInt("soundCounter", _soundCounter);
        });
        _background.Find("soundMinusBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            _soundCounter = Mathf.Clamp(_soundCounter, 0, 4);
            _soundBars[_soundCounter].interactable = false;
            _soundCounter--;
            SoundManager.Instance.DecreaseVolume();
            PlayerPrefs.SetInt("soundCounter", _soundCounter);
        });
    }

    private void HandleMusicButtons()
    {
        _background.Find("musicPlusBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            _musicCounter++;
            _musicCounter = Mathf.Clamp(_musicCounter, 0, 4);
            _musicBars[_musicCounter].interactable = true;
            musicManager.IncreaseVolume();
            PlayerPrefs.SetInt("musicCounter", _musicCounter);
        });
        _background.Find("musicMinusBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            _musicCounter = Mathf.Clamp(_musicCounter, 0, 4);
            _musicBars[_musicCounter].interactable = false;
            _musicCounter--;
            musicManager.DecreaseVolume();
            PlayerPrefs.SetInt("musicCounter", _musicCounter);
        });
    }

    private void HandleStartupBars()
    {
        for (int i = 0; i < _musicBars.Length; i++)
        {
            _musicBars[i].interactable = false;
            _soundBars[i].interactable = false;
        }

        for (int i = 0; i <= _musicCounter; i++)
        {
            _musicBars[i].interactable = true;
        }

        for (int i = 0; i <= _soundCounter; i++)
        {
           _soundBars[i].interactable = true;
        }

    }
}
