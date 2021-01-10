using Assets.ProjectFiles.Scripts.Enums;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;
    private Transform _barTransform;

    private void Awake()
    {
        _barTransform = transform.Find("bar");
    }

    private void Start()
    {
        healthSystem.OnDamage += HealthSysteam_OnDamage;
        UpdateBar();
    }

    private void HealthSysteam_OnDamage()
    {
        UpdateBar();
        SoundManager.Instance.PlaySound(Sounds.EnemyHit);
    }

    private void UpdateBar()
    {
        _barTransform.localScale = new Vector3(healthSystem.GetHealthNormalized(), 1, 1);
    }

    private void OnDisable()
    {
        healthSystem.OnDamage -= HealthSysteam_OnDamage;
    }
}
