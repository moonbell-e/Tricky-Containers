using UnityEngine;
using System;

public partial class DragSystem : MonoBehaviour
{
    public static event Action<DragDirection> DragEvent;

    [Header("Debug")]
    [SerializeField] private Vector2 _tapPosition;
    [SerializeField] private Vector2 _dragEndPosition;
    [SerializeField] private DragDirection _dragDirection;

    [SerializeField] private State _dragState;

    [SerializeField] private float _startDragTime = 0.3f;


    private void Update()
    {
        OnPressed();
        OnDragged();
        OnReleased();
    }

    private void OnPressed()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _tapPosition = Input.mousePosition;
        }
    }
    private void OnDragged()
    {
        if (Input.GetMouseButton(0))
        {
            DetectDrag();
            ExecuteDrag();
        }
    }
    private void OnReleased()
    {
        if(Input.GetMouseButtonUp(0))
        {
            _dragEndPosition = GetDragEndPosition();
            ResetDrag();
        }
    }
    private DragDirection GetDragDirection(Vector2 tapPosition, Vector2 dragEndPosition)
    {
        Vector2 dragDistance = dragEndPosition - tapPosition;
        return dragDistance.x > 0 ? DragDirection.Right : DragDirection.Left;
    }
    private Vector2 GetDragEndPosition()
    {
        if (_dragEndPosition != (Vector2)Input.mousePosition && _dragState == State.Drag)
            return Input.mousePosition;
        else
            return Vector2.zero;
    }

    private void ExecuteDrag()
    {
        while (_dragState == State.Drag && _startDragTime > 0)
        {
            _startDragTime -= Time.deltaTime;
            _dragDirection = GetDragDirection(_tapPosition, _dragEndPosition);
            Drag(_dragDirection);
        }
    }
    private void DetectDrag()
    {
        if(_tapPosition != (Vector2)Input.mousePosition)
            StartDrag();
    }
    private void Drag(DragDirection dragDirection)
    {
        DragEvent?.Invoke(dragDirection);
    }
    private void StartDrag()
    {
        _dragState = State.Drag;
        _startDragTime -= Time.deltaTime;
    }

    private void ResetDrag()
    {
        _dragState = State.None;
        _startDragTime = 0.3f;
    }
}


