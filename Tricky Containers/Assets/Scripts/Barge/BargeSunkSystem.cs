using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BargeSunkSystem : MonoBehaviour
{
    [SerializeField] BargeEntity _currentBarge;

    private void Update()
    {
        _currentBarge.SunkBarge();
    }
}

