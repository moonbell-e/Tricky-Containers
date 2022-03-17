using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class SpawnerBase : MonoBehaviour
{
    public static event Action<T> OnObjectSpawnedEvent;

    [SerializeField] private List<GameObject> _crisperPrefabs;
    [SerializeField] private List<Transform> _points;
    [SerializeField] private CrisperEntity _crisperEntity;

    private void Start()
    {
        SpawnRandomPrefab();
    }
    private void SpawnRandomPrefab()
    {
        int randomPrefabIndex = UnityEngine.Random.Range(0, _crisperPrefabs.Count);
        int randomPointIndex = UnityEngine.Random.Range(0, _points.Count);
        Instantiate(_crisperPrefabs[randomPrefabIndex], _points[randomPointIndex]);
        _crisperEntity = _points[randomPointIndex].GetChild(0).GetComponent<CrisperEntity>();
        SendCrisperEntity(_crisperEntity);
    }

    private void SendCrisperEntity(CrisperEntity crisperEntity)
    {
        
    }
}
