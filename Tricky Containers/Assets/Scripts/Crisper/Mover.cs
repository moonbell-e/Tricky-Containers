using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Mover : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private Rigidbody _rb;

    [SerializeField] private float _speed;
    [SerializeField] private Vector3 MoveDirection;
    private void Awake()
    {
        if (_rb == null)
            _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _rb.AddRelativeForce(MoveDirection * _speed - _rb.velocity);
    }

}


