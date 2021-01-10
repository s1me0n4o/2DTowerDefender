using Assets.ProjectFiles.Scripts.Enums;
using Assets.ProjectFiles.Scripts.Models;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int damage;
    private Transform _target;
    private Rigidbody2D _rb2d;
    private HealthSystem _healthSystem;
    private float _timer;
    private int _buildingLayerMask;
    private float _timerMax = 1f;

    private void Start()
    {
        _target = BuildingManager.Instance.Castle;
        _rb2d = GetComponent<Rigidbody2D>();
        _healthSystem = GetComponent<HealthSystem>();
        _healthSystem.OnDied += HealthSystem_OnDied;

        //to make sure that all spowned enemies together will have different timers
        _timer = Random.Range(0f, _timerMax);
        _buildingLayerMask = LayerMask.GetMask("Building");
    }

    private void HealthSystem_OnDied()
    {
        SoundManager.Instance.PlaySound(Sounds.EnemyDie);
        PoolFactory.DieParticles.Get(transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void Update()
    {
        HandleMovement();
        HandleTargeting();
    }

    private void HandleMovement()
    {
        if (_target != null)
        {
            Vector3 moveDir = (_target.position - transform.position).normalized;
            float speed = 6f;
            _rb2d.velocity = moveDir * speed;
        }
        else
        {
            _rb2d.velocity = Vector2.zero;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var building = collision.gameObject.GetComponent<BuildingTypeHolder>();
        if (building != null)
        {
            var healthsystem = building.GetComponent<HealthSystem>();
            if (healthsystem != null)
            {
                healthsystem.Damage(damage);
            }

            Destroy(gameObject);
        }
    }

    private void LookForTargets()
    {
        float targetMaxRadius = 10f;
        var targets = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius, _buildingLayerMask);
        foreach (var target in targets)
        {
            var newTarget = target.GetComponent<BuildingTypeHolder>();
            if (newTarget != null)
            {
                if (_target == null)
                {
                    _target = newTarget.transform;
                }
                else
                {
                    //compare distances and pick the closets
                    if (Vector3.Distance(transform.position, newTarget.transform.position) < 
                        Vector3.Distance(transform.position, _target.position))
                    {
                        _target = newTarget.transform;
                    }
                }
            }
        }

        if (_target == null)
        {
            _target = BuildingManager.Instance.Castle;
        }
    }

    public static Enemy CreateEnemy(Vector3 position)
    {
        var pfEnemy = Resources.Load<Transform>("pfEnemy");
        var enemy = Instantiate(pfEnemy, position, Quaternion.identity); 
        return enemy.GetComponent<Enemy>();
    }

    private void OnDisable()
    {
        if (_healthSystem != null)
        {
            _healthSystem.OnDied -= HealthSystem_OnDied;
        }
    }
}
