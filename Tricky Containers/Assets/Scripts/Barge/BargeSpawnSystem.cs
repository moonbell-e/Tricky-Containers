using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BargeSpawnSystem : MonoBehaviour
{
    [SerializeField] private List<GameObject> _bargePrefabs;

    private void Start()
    {
        Instantiate(_bargePrefabs[Random.Range(0, _bargePrefabs.Count)], this.transform);
    }
}
