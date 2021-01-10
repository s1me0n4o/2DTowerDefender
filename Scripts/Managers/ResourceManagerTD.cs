using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManagerTD : MonoBehaviour
{
    public static ResourceManagerTD Instance { get; private set; }

    public Action OnResourceAmountChanges;

    [SerializeField] private List<ResourceAmount> startingResourceAmounts;
    private Dictionary<ResourceTypesSO, int> _recourceAmountDict;

    private void Awake()
    {
        #region Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        #endregion

        _recourceAmountDict = new Dictionary<ResourceTypesSO, int>();

        var resourceTypeList = Resources.Load<ListAllResourceTypesSO>(nameof(ListAllResourceTypesSO));

        foreach (var resourceType in resourceTypeList.ListAllResourceTypes)
        {
            _recourceAmountDict.Add(resourceType, 0);
        }

        foreach (var resourceAmount in startingResourceAmounts)
        {
            AddResource(resourceAmount.resourceType, resourceAmount.amount);
        }
    }

    #region public methods
    public void AddResource(ResourceTypesSO resourceType, int amount)
    {
        _recourceAmountDict[resourceType] += amount;
        
        OnResourceAmountChanges?.Invoke();
    }

    public int GetResourceAmount(ResourceTypesSO resourceType)
    {
        _recourceAmountDict.TryGetValue(resourceType, out var result);
        return result;
    }

    public bool HasEnoughGoldToBuild(ResourceAmount[] resourceAmounts)
    {
        foreach (var resource in resourceAmounts)
        {
            if (!(GetResourceAmount(resource.resourceType) >= resource.amount))
            {
                //cannot build
                var logMessage = $"Not enough amount of the reousrce!";
                StartCoroutine(ErrorMessageText.Instance.Show(logMessage));
                return false;
            }
        }
        return true;
    }

    public void SpendResources(ResourceAmount[] resourceAmounts)
    {
        foreach (var amount in resourceAmounts)
        {
            _recourceAmountDict[amount.resourceType] -= amount.amount;
        }
    }

    #endregion
}
