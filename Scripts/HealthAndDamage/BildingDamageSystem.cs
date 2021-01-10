using Assets.ProjectFiles.Scripts.Enums;
using UnityEngine;

public class BildingDamageSystem : MonoBehaviour
{
    private HealthSystem _healthSystem;
    private BuildingTypeSO _buildingType;

    private void Start()
    {
        _buildingType = GetComponent<BuildingTypeHolder>().buildingType;

        _healthSystem = GetComponent<HealthSystem>();
        _healthSystem.SetMaxHealth(_buildingType.MaxHealth, true);

        _healthSystem.OnDied += HealthSystem_OnDied;
    }

    private void HealthSystem_OnDied()
    {
        Destroy(gameObject);
        SoundManager.Instance.PlaySound(Sounds.BuildingDestroyed);
    }

    private void OnDisable()
    {
        _healthSystem.OnDied -= HealthSystem_OnDied;
    }
}
