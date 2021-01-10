using UnityEngine;

public class InfoZoneDetails : MonoBehaviour
{
    private Transform _infoZoneBtn;
    private Transform _infoZonePanel;

    private void Start()
    {
        _infoZoneBtn = transform.Find("infoZoneBtn");
        _infoZonePanel = transform.Find("infoZonePanel");
        _infoZoneBtn.gameObject.SetActive(false);
        _infoZonePanel.gameObject.SetActive(false);
    }

    public void HideInfoZoneBtn()
    {
        _infoZoneBtn.gameObject.SetActive(false);
        _infoZonePanel.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void EscapeTheInfoZone()
    {
        Time.timeScale = 1f;
        _infoZonePanel.gameObject.SetActive(false);
        _infoZoneBtn.gameObject.SetActive(true);
    }

    public void ShowInfoZoneBtn()
    {
        _infoZoneBtn.gameObject.SetActive(true);
    }
}
