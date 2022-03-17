using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Mover : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private Rigidbody _rb;

    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _moveDirection;
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
        var modeDelta = _moveDirection * _speed;
        _rb.MovePosition(_rb.position + modeDelta);
    }

}


