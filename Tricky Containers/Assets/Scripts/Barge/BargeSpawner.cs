using System.Collections.Generic;
using UnityEngine;

public class BargeSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _bargePrefabs;

    private void Awake()
    {
        SpawnRandomPrefab();
    }

    private void SpawnRandomPrefab()
    {
        Instantiate(_bargePrefabs[Random.Range(0, _bargePrefabs.Count)], this.transform);
    }
}
