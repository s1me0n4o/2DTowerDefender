using Assets.ProjectFiles.Scripts;
using Assets.ProjectFiles.Scripts.Models;
using System.Collections;
using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    [SerializeField] int damage;
    
    private Enemy _enemy;
    private Vector3 _lastMoveDirection;
    private float _timeToDie;
    private float _timeToDieMax;
    
    public static ArrowProjectile CreateArrow(Vector3 position, Enemy enemny)
    {
        var arrow = PoolFactory.ArrowPool.Get(position, Quaternion.identity);
        var arrowProjectile =  arrow.GetComponent<ArrowProjectile>();
        arrowProjectile.SetTarget(enemny);
        return arrowProjectile;
    }

    private void Start()
    {
        _timeToDieMax = 7f;
    }

    private void Update()
    {
        Vector3 dir;
        if (_enemy != null)
        {
            dir = (_enemy.transform.position - transform.position).normalized;
            _lastMoveDirection = dir;
        }
        else
        {
            dir = _lastMoveDirection;
        }

        //in 2D for rotation only modify the Z
        transform.eulerAngles = new Vector3(0, 0, Utils.GetAngleFromVector(dir));

        var speed = 20f;
        transform.position += dir * speed * Time.deltaTime;

        _timeToDie -= Time.deltaTime;
        if (_timeToDie <= 0)
        {
            PoolFactory.ArrowPool.Realese(gameObject);
            _timeToDie = _timeToDieMax;
        }
    }

    private void SetTarget(Enemy enemy)
    {
        _enemy = enemy;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.GetComponent<HealthSystem>().Damage(damage);
            PoolFactory.ArrowPool.Realese(this.gameObject);
        }
    }
}
