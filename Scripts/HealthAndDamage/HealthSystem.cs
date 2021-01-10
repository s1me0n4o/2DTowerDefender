using Assets.ProjectFiles.Scripts.Models;
using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public Action OnDamage;
    public Action OnDied;

    [SerializeField]private int _healthAmount;
    [SerializeField]private int _healthAmountMax;

    private void Start() => _healthAmount = _healthAmountMax;

    public void Damage(int damageAmount)
    {
        _healthAmount -= damageAmount;
        _healthAmount = Mathf.Clamp(_healthAmount, 0, _healthAmountMax);
        CameraEffects.Instance.ShakeCamera(20f, 0.15f);
        CameraEffects.Instance.SetVolumeWeight(.7f);

        OnDamage?.Invoke();
        
        if (IsDead())
        {
            CameraEffects.Instance.ShakeCamera(30f, 0.2f);
            CameraEffects.Instance.SetVolumeWeight(1f);
            OnDied?.Invoke();
            PoolFactory.BuildingDestroyParticles.Get(transform.position, Quaternion.identity);
        }
    }

    internal void SetMaxHealth(int maxHealth, bool updateHealth)
    {
        _healthAmountMax = maxHealth;
        if (updateHealth)
        {
            _healthAmount = maxHealth;
        }
    }

    public bool IsDead()
    {
        return _healthAmount <= 0;
    }

    public int GetHealht()
    {
        return _healthAmount;
    }

    public float GetHealthNormalized()
    {
        return Mathf.Clamp((float)_healthAmount / _healthAmountMax, 0, _healthAmountMax);
    }
}
