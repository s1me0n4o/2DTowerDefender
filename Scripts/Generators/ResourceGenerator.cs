using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    private float _repeatTime;
    private float _repeatRate;
    private ResourceGeneartorData _resourceData;
    private int _nearbyResourceAmount;

    private void Awake()
    {
        _repeatTime = 1f;
        _repeatRate = 1f;

        _resourceData = GetComponent<BuildingTypeHolder>().buildingType.ResourceGeneratorData;
        _repeatRate = _resourceData.GenerateResourcePerTime;
    }

    private void Start()
    {

        var collider2DArray = Physics2D.OverlapCircleAll(this.transform.position, _resourceData.ResourceDetectionRadius);

        _nearbyResourceAmount = 0;
        foreach (var collider in collider2DArray)
        {
            var resourceNode = collider.GetComponent<ResourceNode>();
            if (resourceNode != null && resourceNode.resourceType == _resourceData.ResourceType)
            {
                //It is a resource node!
                _nearbyResourceAmount++;
            }
        }

        _nearbyResourceAmount = Mathf.Clamp(_nearbyResourceAmount, 0, _resourceData.MaxResourceAmount);
        if (_nearbyResourceAmount == 0)
        {
            //No resources nearby
            //Disable ResourceGenerator
            this.enabled = false;
        }
        else
        {
            _repeatRate = (_resourceData.GenerateResourcePerTime / 2f) +
                            _resourceData.GenerateResourcePerTime *
                            (1 - (float)_nearbyResourceAmount / _resourceData.MaxResourceAmount);

            InvokeRepeating("UpdateTimer", _repeatTime, _repeatRate);
        }
    }

    private void UpdateTimer()
    {
        ResourceManagerTD.Instance.AddResource(_resourceData.ResourceType, 1);
    }

    public ResourceGeneartorData GetResourceGeneartorData()
    {
        return _resourceData;
    }

    public float GetAmountResourceGeneratedPerSec()
    {
        return _nearbyResourceAmount * (1 / _resourceData.GenerateResourcePerTime);
    }

    public int GetNearbyResourceAmount()
    {
        return _nearbyResourceAmount;
    }
}
