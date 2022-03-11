using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
public class BlockMoveSystem : MonoBehaviour
{
    private Rigidbody _rb;

    public static event Action<BlockMoveSystem> CrisperFellEvent;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

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
        _rb.MovePosition(new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z));
        _rb.useGravity = false;
    }

    private void MoveLeft()
    {
        _rb.MovePosition(new Vector3(transform.position.x - 1f, transform.position.y, transform.position.z));
        _rb.useGravity = false;
    }

    private void MoveDown()
    {
        _rb.useGravity = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Barge"))
        {
            CrisperFellEvent?.Invoke(this);
        }
    }
}
