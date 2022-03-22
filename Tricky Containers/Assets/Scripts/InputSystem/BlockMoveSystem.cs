using UnityEngine;

public class BlockMoveSystem : MonoBehaviour
{
    public CrisperEntity CurrentCrisper;

    private void OnEnable()
    {
        SwipeSystem.SwipeEvent += OnSwipeInput;
    }
    private void OnDisable()
    {
        SwipeSystem.SwipeEvent -= OnSwipeInput;
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
        CurrentCrisper.MoveRight();
    }

    private void MoveLeft()
    {
        CurrentCrisper.MoveLeft();
    }

    private void MoveDown()
    {
        CurrentCrisper.MoveDown();
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
