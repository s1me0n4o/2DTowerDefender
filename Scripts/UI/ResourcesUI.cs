using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesUI : MonoBehaviour
{
    private ListAllResourceTypesSO _resourceTypeList;
    private Transform _resourceTemplate;
    private Dictionary<ResourceTypesSO, Transform> ResourceTypeTransformDict;

    private void Awake()
    {
        _resourceTypeList = Resources.Load<ListAllResourceTypesSO>(nameof(ListAllResourceTypesSO));
        ResourceTypeTransformDict = new Dictionary<ResourceTypesSO, Transform>();

        _resourceTemplate = transform.Find("ResourceTemplate");
        _resourceTemplate.gameObject.SetActive(false);

        var index = 0;
        foreach (var resource in _resourceTypeList.ListAllResourceTypes)
        {
            var resourceTransform = Instantiate(_resourceTemplate, this.transform);
            resourceTransform.gameObject.SetActive(true);

            var offsetX = -180f;
            resourceTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetX * index, 0);

            resourceTransform.Find("Image").GetComponent<Image>().sprite = resource.Sprite;
            resourceTransform.Find("Text").GetComponent<Text>().text = "0";

            ResourceTypeTransformDict[resource] = resourceTransform;
            index++;
        }

    }

    private void Start()
    {
        ResourceManagerTD.Instance.OnResourceAmountChanges += ResourceManager_OnResourceAmountChanges;
    }

    private void ResourceManager_OnResourceAmountChanges()
    {
        UpdateResourceAmount();
    }

    private void UpdateResourceAmount()
    {
        foreach (var resource in _resourceTypeList.ListAllResourceTypes)
        {
            ResourceTypeTransformDict.TryGetValue(resource, out var resourceTemplate);
            if (resourceTemplate == null)
            {
                return;
            }

            var resourceAmount = ResourceManagerTD.Instance.GetResourceAmount(resource);
            resourceTemplate.Find("Text").GetComponent<Text>().text = resourceAmount.ToString();
        }
    }
    private void OnDisable()
    {
        ResourceManagerTD.Instance.OnResourceAmountChanges -= ResourceManager_OnResourceAmountChanges;
    }
}
