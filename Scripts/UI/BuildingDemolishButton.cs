using UnityEngine;
using UnityEngine.UI;

public class BuildingDemolishButton : MonoBehaviour
{
    [SerializeField]private BuildingTypeHolder building;

    private void Awake()
    {
       transform.Find("button").GetComponent<Button>().onClick.AddListener(() =>
       {
           var resourceAmounts = building.buildingType.BuildingCosts;
           foreach (var resource in resourceAmounts)
           {
               ResourceManagerTD.Instance.AddResource(resource.resourceType, Mathf.FloorToInt(resource.amount * 0.7f));
           }
           Destroy(building.gameObject);
       });
    }
}
