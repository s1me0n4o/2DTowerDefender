using UnityEngine;

public class ConstructionTimerUI : MonoBehaviour
{

    [SerializeField]private BuildingConstruction buildingConstruction;

    private Transform _rotatingCircle;

    private void Awake() => _rotatingCircle = transform.Find("RotatingCircle").transform;

    private void Start()
    {
        BuildingConstruction.OnConstructionStart += BuildingConstruction_OnConstructionStart;
        buildingConstruction.OnConstructionReady += BuildingConstruction_OnConstructionReady;
    }

    private void BuildingConstruction_OnConstructionStart()
    {
        _rotatingCircle.gameObject.SetActive(true);
    }

    private void BuildingConstruction_OnConstructionReady()
    {
        _rotatingCircle.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        BuildingConstruction.OnConstructionStart -= BuildingConstruction_OnConstructionStart;
        buildingConstruction.OnConstructionReady -= BuildingConstruction_OnConstructionReady;
    }
}
