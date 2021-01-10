using UnityEngine;
using UnityEngine.UI;

public class ResourceGeneratorOverlay : MonoBehaviour
{
    [SerializeField] private ResourceGenerator resourceGenerator;

    private void Start()
    {
        var rsData = resourceGenerator.GetResourceGeneartorData();
        transform.Find("icon").GetComponent<SpriteRenderer>().sprite = rsData.ResourceType.Sprite;
        var canvas = transform.Find("Canvas");
        canvas.transform.Find("resourcePerMinText").GetComponent<Text>().text = resourceGenerator.GetAmountResourceGeneratedPerSec().ToString();
        var percentOfResourceAmount = Mathf.RoundToInt(resourceGenerator.GetNearbyResourceAmount() / 
                                                       (float)resourceGenerator.GetResourceGeneartorData().MaxResourceAmount * 100f);
        canvas.transform.Find("efficiencyText").GetComponent<Text>().text = $"{percentOfResourceAmount.ToString()}%";
    }
}
