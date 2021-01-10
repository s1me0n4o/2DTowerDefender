using Assets.ProjectFiles.Scripts.Enums;
using Assets.ProjectFiles.Scripts.Models;
using System;
using UnityEngine;

public class BuildingConstruction : MonoBehaviour
{
    public static Action OnConstructionStart;
    public Action OnConstructionReady;

    private float _timer;
    private BuildingTypeSO _buildingType;
    private float _timerMax;
    private BoxCollider2D _boxCollider2D;
    private SpriteRenderer _spriteRenderer;
    private BuildingTypeHolder _buildingTypeHolder;
    private Material _constructionMaterial;
    private static Transform _buildingPlacedParticles;

    private void Awake() 
    { 
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _spriteRenderer = transform.Find("sprite").GetComponent<SpriteRenderer>();
        _buildingTypeHolder = GetComponent<BuildingTypeHolder>();
        _constructionMaterial =_spriteRenderer.material;

    }

    public static BuildingConstruction CreateBuildingConstruction(Vector3 position, BuildingTypeSO type)
    {
        var construction = PoolFactory.BuildingConstructionPool.Get(position, Quaternion.identity);
        PoolFactory.BuildingPlacedParticles.Get(position, Quaternion.identity);
        OnConstructionStart?.Invoke();

        var buildingConstruction = construction.GetComponent<BuildingConstruction>();
        buildingConstruction.SetupBuildingType(type);
        return buildingConstruction;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        _constructionMaterial.SetFloat("_Progress", GetConstructionTimerNormalized());
        if (_timer <= 0)
        {
            _timer = _timerMax;
            Instantiate(_buildingType.Prefab, transform.position, Quaternion.identity);
            PoolFactory.BuildingPlacedParticles.Get(transform.position, Quaternion.identity);
            SoundManager.Instance.PlaySound(Sounds.BuildingPlaced);
            OnConstructionReady?.Invoke();
            PoolFactory.BuildingConstructionPool.Realese(gameObject);
        }
    }

    private float GetConstructionTimerNormalized()
    {
        return 1 - _timer / _timerMax;
    }

    private void SetupBuildingType(BuildingTypeSO buildingType)
    {
        _timerMax = buildingType.ConstructionTimerMax;
        _timer = _timerMax;
        _buildingType = buildingType;
        _spriteRenderer.sprite = buildingType.Sprite;

        var buildingBoxColider2D = buildingType.Prefab.GetComponent<BoxCollider2D>();
        _boxCollider2D.offset = buildingBoxColider2D.offset;
        _boxCollider2D.size = buildingBoxColider2D.size;
        _buildingTypeHolder.buildingType = buildingType;
    }
}
