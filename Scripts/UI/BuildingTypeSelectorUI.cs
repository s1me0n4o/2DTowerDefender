using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectorUI : MonoBehaviour
{
    [SerializeField] private Sprite handSprite;

    private ListAllBuildingTypesSO _buildingTypeList;
    private Dictionary<BuildingTypeSO, Transform> BtnTransformDict;
    private Transform handBtn;

    private void Awake()
    {
        var btnTemplate = transform.Find("btnTemplate");
        btnTemplate.gameObject.SetActive(false);
        
        _buildingTypeList = Resources.Load<ListAllBuildingTypesSO>(nameof(ListAllBuildingTypesSO));
        BtnTransformDict = new Dictionary<BuildingTypeSO, Transform>();

        //setting hand btn 
        var index = 0;
        handBtn = Instantiate(btnTemplate, transform);
        handBtn.gameObject.SetActive(true);

        var offsetX = 150f;
        handBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetX * index, 0);
        
        handBtn.Find("BackgroundImage").GetComponent<Image>().color = Color.white;

        handBtn.Find("BtnImage").GetComponent<Image>().sprite = handSprite;
        handBtn.Find("CostPanel").gameObject.SetActive(false);
        handBtn.GetComponent<Button>().onClick.AddListener(() =>
        {
            BuildingManager.Instance.SetActiveBuildingType(null);
        });
        index++;

        //setting all other btns other btns in dict
        foreach (var type in _buildingTypeList.BuildingTypeList)
        {
            var btnTransform = Instantiate(btnTemplate, transform);
            btnTransform.gameObject.SetActive(true);

            btnTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetX * index, 0);

            btnTransform.Find("BtnImage").GetComponent<Image>().sprite = type.Sprite;
            var costPanel = btnTransform.Find("CostPanel");
            var costObj = costPanel.Find("CostObject").GetComponent<Transform>();
            var counter = 0;
            foreach (var item in type.BuildingCosts)
            {
                if (counter > 0)
                {
                    var newCostObj = Instantiate(costObj);
                    newCostObj.transform.SetParent(costPanel);
                    newCostObj.localScale = new Vector3(1, 1, 1);
                }
                costObj.Find("costImage").GetComponent<Image>().sprite = item.resourceType.Sprite;
                costObj.Find("costText").GetComponent<TextMeshProUGUI>().text = item.amount.ToString();
                counter++;
            }

            btnTransform.GetComponent<Button>().onClick.AddListener(() => 
            {
                BuildingManager.Instance.SetActiveBuildingType(type);
            });

            BtnTransformDict[type] = btnTransform;

            index++;
        }
    }

    private void Start()
    {
        BuildingManager.Instance.OnCastleSpown += BuildingManager_OnActiveBuildingTypeChanged;
        BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuildingTypeChanged;
    }

    private void BuildingManager_OnActiveBuildingTypeChanged()
    {
        UpdateButtonActiveBuildingType();
    }

    private void UpdateButtonActiveBuildingType()
    {
        var handGO = handBtn.Find("SelectedBorder").gameObject;
        handGO.SetActive(false);
        foreach (var bType in BtnTransformDict.Keys)
        {
            BtnTransformDict.TryGetValue(bType, out var btnTransform);
            if (btnTransform == null)
            {
                return;
            }
            btnTransform.Find("SelectedBorder").gameObject.SetActive(false);
        }

        var activeBuildingType = BuildingManager.Instance.GetActiveBuildingType();
        if (activeBuildingType == null)
        {
            handGO.SetActive(true);
        }
        else
        {
            BtnTransformDict[activeBuildingType].Find("SelectedBorder").gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        BuildingManager.Instance.OnCastleSpown -= BuildingManager_OnActiveBuildingTypeChanged;
        BuildingManager.Instance.OnActiveBuildingTypeChanged -= BuildingManager_OnActiveBuildingTypeChanged;
    }
}
