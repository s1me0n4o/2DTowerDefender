using System;
using UnityEngine;

public class CastleSpowner : MonoBehaviour
{

    [SerializeField] private ListAllBuildingTypesSO castles;
    [SerializeField] private Transform castleSelectorUI;
    [SerializeField] private Transform infoZone;
    [SerializeField] private Transform OptionMenu;
    
    private InfoZoneDetails _infoDetails;
    private OptionsMenuUI _optionMenu;

    private void Awake()
    {
        castleSelectorUI.gameObject.SetActive(true);
        _infoDetails = infoZone.GetComponent<InfoZoneDetails>();
        _optionMenu = OptionMenu.GetComponent<OptionsMenuUI>();
    }

    private Transform SpownACasle(Transform prefab)
    {
        var castle = Instantiate(prefab, this.gameObject.transform.position, Quaternion.identity);
        castle.transform.SetParent(this.transform);
        _infoDetails.ShowInfoZoneBtn();
        _optionMenu.ShowOptionsBtn();
        Time.timeScale = 0f;
        return castle;
    }
    
    public void GetBtnIndex(string btnIndex)
    {
        int.TryParse(btnIndex, out int result);
        var castle = SpownACasle(castles.BuildingTypeList[result].Prefab);
        castleSelectorUI.gameObject.SetActive(false);
        Time.timeScale = 1f;
        BuildingManager.Instance.OnCastleSpown?.Invoke();
        BuildingManager.Instance.SetCastle(castle);
    }
}
