using System.Collections.Generic;
using UnityEngine;
using System;

public class BargeSpawner : MonoBehaviour
{
    public static event Action<BargeEntity, BargeSpawner> OnBargeSpawnedEvent;

    [SerializeField] private List<GameObject> _bargePrefabs;
    [SerializeField] private Transform _container;

    private void Awake()
    {
        OnBargeSpawnedEvent?.Invoke(SpawnRandom(), this);
    }


    public BargeEntity SpawnRandom()
    {
        return Instantiate(_bargePrefabs[0], _container).GetComponent<BargeEntity>();
    }
}
