using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;

public class CrisperSpawner : MonoBehaviour
{
    public static event Action<CrisperSpawner> OnCrisperSpawnedEvent;
    public static event Action<int> OnCrisperCountEvent;

    public CrisperEntity CrisperEntity;

    [SerializeField] private List<GameObject> _crisperPrefabs;
    [SerializeField] private Transform _container;
    [SerializeField] private GameObject _crisperSpawnEffect;
    [SerializeField] private List<GameObject> _spawnedCrispers;
    [SerializeField] private List<GameObject> _neededToSpawnCrispers;
    [SerializeField] private GameObject _lastSpawnedCrisper;
    [SerializeField] private GameObject _tractor;
    [SerializeField] private bool _isSpawnTractor;
    [SerializeField] private bool _isSpawnNeeded;

    [SerializeField] private int _crisperCounter;
    [SerializeField] private bool _isTriggered;

    private int i = -1;
    public int CrisperCounter => _crisperCounter;
    public GameObject LastSpawnedCrisper => _lastSpawnedCrisper;

    [Header("Waypoints")]
    [SerializeField] private float _waypointStep;
    [SerializeField] private int maximumWaypoint;
    [SerializeField] private int minimumWaypoint;
    [SerializeField] private Vector3 _spawnWaypoint;

    private void Awake()
    {
        if (_isTriggered) return;
        _isTriggered = true;

        OnCrisperSpawnedEvent?.Invoke(this);
        IncreaseBorders(ref minimumWaypoint, ref maximumWaypoint);
    }

    public void SpawnRandom()
    {
        StartCoroutine(SpawnRandomCoroutine());
        IncreaseCrisperCounter();
    }
    private Vector3 GetWaypoint()
    {
        _spawnWaypoint = new Vector3(GetRandomWaypoint(), 11.5f);
        return _spawnWaypoint;
    }
    private float GetRandomWaypoint()
    {
        var randomWaypoint = ((float)UnityEngine.Random.Range(maximumWaypoint, minimumWaypoint)) * _waypointStep;
        return randomWaypoint;
    }

    private IEnumerator SpawnRandomCoroutine()
    {
        int randomPrefabIndex = UnityEngine.Random.Range(0, _crisperPrefabs.Count);
        Vector3 randomPosition = GetWaypoint();
        if (_isSpawnTractor)
        {
            GameObject spawnedCrisper = Instantiate(_tractor, GetWaypoint(), Quaternion.identity, _container);
            spawnedCrisper.transform.Rotate(0f, -90f, 0f);
            _spawnedCrispers.Add(spawnedCrisper);
            _lastSpawnedCrisper = GetLastSpawnedCrisper();
            GetSpawnParticles(spawnedCrisper).Play();
            CrisperEntity = GetCrisperEntity(spawnedCrisper);
        }
        else if(_isSpawnNeeded)
        {
            GameObject spawnedCrisper = Instantiate(_neededToSpawnCrispers[++i], randomPosition, _container.rotation, _container);
            _spawnedCrispers.Add(spawnedCrisper);
            _lastSpawnedCrisper = GetLastSpawnedCrisper();
            GetSpawnParticles(spawnedCrisper).Play();
            CrisperEntity = GetCrisperEntity(spawnedCrisper);
        }
        else
        {
            GameObject spawnedCrisper = Instantiate(_crisperPrefabs[randomPrefabIndex], randomPosition, _container.rotation, _container);
            _spawnedCrispers.Add(spawnedCrisper);
            _lastSpawnedCrisper = GetLastSpawnedCrisper();
            GetSpawnParticles(spawnedCrisper).Play();
            CrisperEntity = GetCrisperEntity(spawnedCrisper);
        }
        yield return null;
    }
    private void IncreaseCrisperCounter()
    {
        _crisperCounter++;
        OnCrisperCountEvent?.Invoke(_crisperCounter);
    }

    private void IncreaseBorders(ref int min, ref int max)
    {
        min *= 2;
        max *= 2;
    }
    private CrisperEntity GetCrisperEntity(GameObject spawnedCrisper)
    {
        return spawnedCrisper.GetComponent<CrisperEntity>();
    }
    private ParticleSystem GetSpawnParticles(GameObject spawnedCrisper)
    {
        _crisperSpawnEffect.transform.position = spawnedCrisper.transform.position;
        return _crisperSpawnEffect.GetComponent<ParticleSystem>();
    }

    private GameObject GetLastSpawnedCrisper()
    {
        if(_spawnedCrispers.Count == 1)
            return _spawnedCrispers[0];
        else
            return _spawnedCrispers[_spawnedCrispers.Count - 2];

    }
}

