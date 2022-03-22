using UnityEngine;

public class BargeEntity : MonoBehaviour
{
    [SerializeField] private float _timer = 2f;
    [SerializeField] private bool _isShouldBend;

    public bool IsShouldBend => _isShouldBend;
    public void BendBarge()
    {
        if (_timer > 0)
        {
            transform.Rotate(new Vector3(0f, 0f, GameManager.Instance.LastCrisperPosition) * Time.deltaTime);
            _timer -= Time.deltaTime;
        }
    }

    public void MakeBend()
    {
        _timer = 2f;
        _isShouldBend = true;
    }

    public void SunkBarge()
    {
        if (transform.rotation.z > 0.18f || (transform.rotation.z < -0.21f))
        {
            GameManager.Instance.IsLost = true;
            transform.Translate(Vector3.down * Time.deltaTime);
        }
    }
}
