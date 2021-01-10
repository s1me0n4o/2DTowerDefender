using System;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField]private float _shootTimerMax;
    [SerializeField]private float targetMaxRadius;

    private float _shootTimer;
    private Enemy _target;
    private float _timer;
    private float _timerMax = .2f;
    private Vector3 _projectileSpownPos;
    private int _enemyLayerMask;

    private void Awake() => _projectileSpownPos = transform.Find("ArrowSpownPoint").position;

    private void Start() => _enemyLayerMask = LayerMask.GetMask("Enemy");

    private void Update()
    {
        HandleTargeting();
        HandleShooting();
    }

    private void HandleShooting()
    {
        _shootTimer -= Time.deltaTime;
        if (_shootTimer <= 0f)
        {
            _shootTimer = _shootTimerMax;
            if (_target != null)
            {
                ArrowProjectile.CreateArrow(_projectileSpownPos, _target);
            }
        }
    }

    private void HandleTargeting()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            _timer += _timerMax;
            LookForTargets();
        }
    }

    private void LookForTargets()
    {
        var targets = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius, _enemyLayerMask);
        foreach (var target in targets)
        {
            if (target.gameObject.activeInHierarchy)
            {
                var newTarget = target.GetComponent<Enemy>();
                if (newTarget != null)
                {
                    if (_target == null)
                    {
                        _target = newTarget;
                    }
                    else
                    {
                        //compare distances and pick the closets
                        if (Vector3.Distance(transform.position, newTarget.transform.position) <
                            Vector3.Distance(transform.position, _target.transform.position))
                        {
                            _target = newTarget;
                        }
                    }
                }
            }
        }
    }   
}
