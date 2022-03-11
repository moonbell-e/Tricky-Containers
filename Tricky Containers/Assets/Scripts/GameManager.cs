using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Shaker _cameraShaker;
    private void OnEnable()
    {
        BlockMoveSystem.CrisperFellEvent += OnCrisperFall;
    }

    private void OnDisable()
    {
        BlockMoveSystem.CrisperFellEvent -= OnCrisperFall;
    }

    private void OnCrisperFall(BlockMoveSystem blockMoveSystem)
    {
        blockMoveSystem.enabled = false;
        _cameraShaker.ShakeCamera();
    }
}
