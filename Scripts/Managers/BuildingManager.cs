using Assets.ProjectFiles.Scripts;
using Assets.ProjectFiles.Scripts.Enums;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }
    public Transform Castle { get => _castle; private set { _castle = value; } }

    public Action OnCastleSpown;
    public Action OnActiveBuildingTypeChanged;

    [SerializeField] private BuildingTypeSO activeBuildingType;
    [SerializeField] private GameOverUI gameOverUI;

    private Camera _mainCamera;
    private HealthSystem _castleHealthSystem;
    private ListAllBuildingTypesSO _activeBuildingTypeList;
    private Transform _castle;

    private void Awake()
    {
        #region Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        #endregion

        _activeBuildingTypeList = Resources.Load<ListAllBuildingTypesSO>(nameof(ListAllBuildingTypesSO));
    }

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void BuildingManager_OnDied()
    {
        SoundManager.Instance.PlaySound(Sounds.GameOver);
        gameOverUI.ShowPanel();
        StopAllCoroutines();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (activeBuildingType != null && CanSpownBuilding(activeBuildingType, Utils.GetMouseWorldPosition()))
            {
                if (ResourceManagerTD.Instance.HasEnoughGoldToBuild(activeBuildingType.BuildingCosts))
                {
                    ResourceManagerTD.Instance.SpendResources(activeBuildingType.BuildingCosts);
                    BuildingConstruction.CreateBuildingConstruction(Utils.GetMouseWorldPosition(), activeBuildingType);
                    SoundManager.Instance.PlaySound(Sounds.BuildingPlaced);
                }
            }
        }
    }

    private bool CanSpownBuilding(BuildingTypeSO bType, Vector3 pos)
    {
        var boxCollider = bType.Prefab.GetComponent<BoxCollider2D>();

        var collidersArray = Physics2D.OverlapBoxAll(pos + (Vector3)boxCollider.offset, boxCollider.size, 0);
        var isAreaClear = collidersArray.Length == 0;
        if (!isAreaClear)
        {
            var logMessage = "The field is already taken!";
            StartCoroutine(ErrorMessageText.Instance.Show(logMessage));
            return false;
        }

        collidersArray = Physics2D.OverlapCircleAll(pos, bType.MinConstructionRadios);
        foreach (var collider in collidersArray)
        {
            //colliders iside a construction radios
            var buildingTypeHolder = collider.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder != null)
            {
                if (buildingTypeHolder.buildingType = bType)
                {
                    //There is already a building of this type in the construction radios!
                    var logMessage = "Too close to building of the same type!";
                    StartCoroutine(ErrorMessageText.Instance.Show(logMessage));
                    return false;
                }
            }
        }

        return true;
    }

    #region public methods
    public void SetActiveBuildingType(BuildingTypeSO bType)
    {
        activeBuildingType = bType;

        OnActiveBuildingTypeChanged?.Invoke();
    }

    public BuildingTypeSO GetActiveBuildingType()
    {
        return activeBuildingType;
    }

    public void SetCastle(Transform castle)
    {
        _castle = castle;
        _castleHealthSystem = _castle.GetComponent<HealthSystem>();
        _castleHealthSystem.OnDied += BuildingManager_OnDied;
    }
    #endregion
}
