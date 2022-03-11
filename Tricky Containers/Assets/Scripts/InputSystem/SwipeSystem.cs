using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SwipeSystem : MonoBehaviour
{
    public static event Action<SwipeDirection> SwipeEvent;

    [Header("Debug")]
    [SerializeField] Vector2 _tapPosition;
    [SerializeField] Vector2 _swipeDelta;

    [SerializeField] private float _triggerSwipeDelta = 20f;

    [SerializeField] private State IsSwiping;

    private void Update()
    {
        ButtonDown();
        ButtonUp();
        GetSwipeDelta();
        TrySwipe();
    }
    private void GetSwipeDelta()
    {
        if (IsSwiping == State.Swipe)
        {
            if (Input.GetMouseButton(0))
                _swipeDelta = (Vector2)Input.mousePosition - _tapPosition;
        }
        else
        {
            _swipeDelta = Vector2.zero;
        }
    }
    private void GetSwipeDirection()
    {
        if (Mathf.Abs(_swipeDelta.x) > Mathf.Abs(_swipeDelta.y))
        {
            SwipeEvent?.Invoke(_swipeDelta.x > 0 ? SwipeDirection.Right : SwipeDirection.Left);
        }
        else
            SwipeEvent?.Invoke(SwipeDirection.Down);
    }
    private bool TrySwipe()
    {
        bool _isShouldSwipe = _swipeDelta.magnitude > _triggerSwipeDelta;


        if (_isShouldSwipe)
        {
            Swipe();
            ResetSwipe();
            return true;
        }
        else
        {
            return false;
        }
    }
    private void ButtonDown()
    {
        StartSwipe();
    }
    private void ButtonUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
            _tapPosition = Input.mousePosition;
            ResetSwipe();
        }
    }
    private void StartSwipe()
    {
        if(Input.GetMouseButtonDown(0))
        {
            IsSwiping = State.Swipe;
            _tapPosition = Input.mousePosition;
        }
    }
    private void Swipe()
    {
        GetSwipeDirection();
    }
    private void ResetSwipe()
    {
        IsSwiping = State.None;
        _tapPosition = Vector2.zero;
        _swipeDelta = Vector2.zero;
    }
}
