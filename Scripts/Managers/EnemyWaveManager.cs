using Assets.ProjectFiles.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    public static EnemyWaveManager Instance { get; private set; }

    public Action<float> OnEnemySpown;
    public Action<int> OnWaveIndexChange;

    [SerializeField] private List<Transform> spownPosition;
    [SerializeField] private Transform nextSpownPosition;
    [SerializeField] private float _respownRate = 10;

    private int _waveIndex = 0;
    private int _enemyCount = 5;
    private Vector3 _spownPos;

    private void Awake()
    {
        #region Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        #endregion
    }

    private void Start()
    {
        BuildingManager.Instance.OnCastleSpown += CastleSpowner_OnCastleSpown;
    }

    private void CastleSpowner_OnCastleSpown()
    {
        _spownPos = spownPosition[UnityEngine.Random.Range(0, spownPosition.Count)].position;
        nextSpownPosition.position = _spownPos;

        StartCoroutine(SpownEnemies());
    }

    private IEnumerator SpownEnemies()
    {
        while (true)
        {
            OnWaveIndexChange?.Invoke(_waveIndex);
            OnEnemySpown?.Invoke(_respownRate);
            yield return new WaitForSeconds(_respownRate);
            for (int i = 0; i < _enemyCount; i++)
            {
                var randomDistance = UnityEngine.Random.Range(0, 10f);
                Enemy.CreateEnemy(_spownPos + Utils.GetRandomDirection() * randomDistance);
                yield return new WaitForSeconds(0.3f);
            }
            _spownPos = spownPosition[UnityEngine.Random.Range(0, spownPosition.Count)].position;
            nextSpownPosition.position = _spownPos;
            _waveIndex++;
            _enemyCount += 1 * _waveIndex;
        }
    }

    public Vector3 GetNextSpownPos()
    {
        return nextSpownPosition.position;
    }

    public int GetWaveIndex()
    {
        return _waveIndex;
    }

    private void OnDisable()
    {
        BuildingManager.Instance.OnCastleSpown -= CastleSpowner_OnCastleSpown;
    }
}
