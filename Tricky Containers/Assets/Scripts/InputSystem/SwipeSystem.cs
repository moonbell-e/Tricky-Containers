using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SwipeSystem : MonoBehaviour
{
    public static event Action<SwipeDirection> SwipeEvent;

    [Header("Debug")]
    [SerializeField] private Vector2 _tapPosition;
    [SerializeField] private Vector2 _swipeDelta;
    [SerializeField] private SwipeDirection _swipeDirection;

    [SerializeField] private float _triggerSwipeDelta = 20f;

    [SerializeField] private State _swipeState;

    private void Update()
    {
        OnButtonDown();
        OnButtonUp();
        TrySwipe();
    }

    private void GetInput()
    {
        _swipeDelta = GetSwipeDelta();
        _swipeDirection = GetSwipeDirection(_swipeDelta);
    }
    private Vector2 GetSwipeDelta()
    {
        if (_swipeState == State.Swipe)
            return (Vector2)Input.mousePosition - _tapPosition;

        return (Vector2.zero);
    }
    private SwipeDirection GetSwipeDirection(Vector2 swipeDelta)
    {
        if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
            return swipeDelta.x > 0 ? SwipeDirection.Left : SwipeDirection.Right;

        return SwipeDirection.Down;
    }
    private bool DetectSwipe(SwipeDirection swipeDirection)
    {
        bool isShouldSwipe = _swipeDelta.magnitude > _triggerSwipeDelta;

        if (isShouldSwipe)
        {
            Swipe(swipeDirection);
            return true;
        }

        return false;
    }
    private void OnButtonDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartSwipe();
        }
    }
    private void OnButtonUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
            ResetSwipe();
        }
    }

    private void TrySwipe()
    {
        GetInput();
        if (DetectSwipe(_swipeDirection))
            ResetSwipe();
    }
    private void Swipe(SwipeDirection swipeDirection)
    {
        SwipeEvent?.Invoke(swipeDirection);
    }

    private void StartSwipe()
    {
        _swipeState = State.Swipe;
        _tapPosition = Input.mousePosition;
    }
    private void ResetSwipe()
    {
        _swipeState = State.None;
        _tapPosition = Vector2.zero;
        _swipeDelta = Vector2.zero;
    }
}
