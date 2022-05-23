using UnityEngine;
using System;

//[RequireComponent(typeof(Rigidbody))]
public class CrisperEntity : MonoBehaviour
{
    public static event Action<CrisperEntity, Collision> OnHitCollider;

    [SerializeField] private GameObject _lightning;
    [SerializeField] private bool _isTriggered;
    [SerializeField] private bool _isAccelerated;

    private Rigidbody _rb;
    private float _fallSpeed = 20f;

    private void Awake()
    {
        if (_rb == null)
            _rb = GetComponent<Rigidbody>();

        _fallSpeed = 30f;
    }

    public void MoveRight()
    {
        var moveDelta = new Vector3(0.5f, 0f, 0f);
        _rb.MovePosition(_rb.position + moveDelta);
    }

    public void MoveLeft()
    {
        var moveDelta = new Vector3(-0.5f, 0f, 0f);
        _rb.MovePosition(_rb.position + moveDelta);
    }
    public void MoveDown()
    {
        //_rb.useGravity = _isAccelerated;
        _rb.AddForce(new Vector3(0f, -1f * _fallSpeed, 0f), ForceMode.VelocityChange);
    }

    public void ShowLightning(bool isShown)
    {
        _lightning.SetActive(isShown);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (_isTriggered) return;
        _isTriggered = true;
        OnHitCollider?.Invoke(this, collision);
    }
}
