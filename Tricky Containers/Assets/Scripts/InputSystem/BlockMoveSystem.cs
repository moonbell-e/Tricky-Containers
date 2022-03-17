using UnityEngine;

public class BlockMoveSystem : MonoBehaviour
{
    [SerializeField] private CrisperEntity _currentCrisper;

    private void OnEnable()
    {
        SwipeSystem.SwipeEvent += OnSwipeInput;
        CrisperSpawner.CrisperSpawnEvent += OnCrisperSpawn; 
    }
    private void OnDisable()
    {
        SwipeSystem.SwipeEvent -= OnSwipeInput;
        CrisperSpawner.CrisperSpawnEvent -= OnCrisperSpawn;
    }

    private void OnSwipeInput(SwipeDirection swipeDirection)
    {
        switch (swipeDirection)
        {
            case SwipeDirection.Down:
                {
                    MoveDown();
                    break;
                }
            case SwipeDirection.Right:
                {
                    MoveRight();
                    break;
                }
            case SwipeDirection.Left:
                {
                    MoveLeft();
                    break;
                }
        }
    }
    private void MoveRight()
    {
        _currentCrisper.MoveRight();
    }

    private void MoveLeft()
    {
        _currentCrisper.MoveLeft();
    }

    private void MoveDown()
    {
        _currentCrisper.MoveDown();
    }

    private void OnCrisperSpawn(CrisperEntity crisper)
    {
        _currentCrisper = crisper;
    }

    public void DisablePlayerInput()
    {
        this.enabled = false;
    }

    //private void OnEnable()
    //{
    //    DragSystem.DragEvent += OnDragInput;
    //}
    //private void OnDisable()
    //{
    //    DragSystem.DragEvent -= OnDragInput;
    //}
    //private void OnDragInput(DragDirection dragDirection)
    //{
    //    switch (dragDirection)
    //    {
    //        case DragDirection.Right:
    //            {
    //                MoveRight();
    //                break;
    //            }
    //        case DragDirection.Left:
    //            {
    //                MoveLeft();
    //                break;
    //            }
    //    }
    //}
}
