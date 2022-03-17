using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BargeAngleSystem : MonoBehaviour
{
    [SerializeField] BargeEntity _currentBarge;
    private void Update()
    {
        if(_currentBarge.IsShouldBend)
            _currentBarge.BendBarge();
    }
}
