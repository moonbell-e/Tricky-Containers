using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
public class CrisperEntity : MonoBehaviour
{
    [SerializeField] private GameObject _lightning;
    private Rigidbody _rb;
    private float _fallSpeed;
    [SerializeField] private bool isAccelerated;

    public static event Action<CrisperEntity, Collision> OnHitCollider;
    public GameObject Lightning => _lightning;

    private void Awake()
    {
        if (_rb == null)
            _rb = GetComponent<Rigidbody>();

        _fallSpeed = 15f;
    }


    public void MoveRight()
    {
        var moveDelta = new Vector3(1f, 0f, 0f);
        _rb.MovePosition(_rb.position + moveDelta);
    }

    public void MoveLeft()
    {
        var moveDelta = new Vector3(-1f, 0f, 0f);
        _rb.MovePosition(_rb.position + moveDelta);
    }
    public void MoveDown()
    {
        _rb.AddRelativeForce(new Vector3(0f, -1f * _fallSpeed, 0f), ForceMode.VelocityChange);
        _rb.useGravity = isAccelerated;
    }

    public void ShowLightning(bool isShown)
    {
        Lightning.SetActive(isShown);
    }

    public void OnCollisionEnter(Collision collision)
    {
        OnHitCollider?.Invoke(this, collision);
    }

    //public void DragMoveRight()
    //{
    //    _rb.MovePosition(new Vector3(transform.position.x + 0.5f, transform.position.y));
    //}

    //public void DragMoveLeft()
    //{
    //    _rb.MovePosition(new Vector3(transform.position.x - 0.5f, transform.position.y));
    //}
}
