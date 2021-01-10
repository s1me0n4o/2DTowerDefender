using UnityEngine;

/// <summary>
/// Helper script in order to contain the reference building type
/// </summary>
public class BuildingTypeHolder : MonoBehaviour
{
    public BuildingTypeSO buildingType;
    private Transform _demolishBtn;
    private bool _demolishBtnCurrentStatus;

    private void Awake() 
    {
        _demolishBtn = transform.Find("pfDemolishButton");
        ChangeDemolishBtnStatus(_demolishBtnCurrentStatus);
    }

    private void OnMouseDown()
    {
        ChangeDemolishBtnStatus(_demolishBtnCurrentStatus);
    }

    private void ChangeDemolishBtnStatus(bool status)
    {
        if (_demolishBtn != null)
        {
            _demolishBtn.gameObject.SetActive(status);
            _demolishBtnCurrentStatus = !_demolishBtnCurrentStatus;
        }
    }
}
