using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BargeEntity : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private float _timer = 2f;
    [SerializeField] private bool _isShouldBend;

    public bool IsShouldBend => _isShouldBend;
    public void BendBarge()
    {
        if (_timer > 0)
        {
            transform.Rotate(new Vector3(0f, 0f, _gameManager._lastCrisperPosition) * Time.deltaTime);
            _timer -= Time.deltaTime;
        }
    }

    public void MakeBend()
    {
        _isShouldBend = true;
    }
}
