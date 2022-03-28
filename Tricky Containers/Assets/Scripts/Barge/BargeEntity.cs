using UnityEngine;

public class BargeEntity : MonoBehaviour
{
    [SerializeField] private float _bendTimer = 1f;
    [SerializeField] private float _afterBendTimer = 0.4f;
    [SerializeField] private bool _isShouldBend;
    [SerializeField] private float _previousRot;

    public bool IsShouldBend => _isShouldBend;
    public void BendBarge()
    {
        if (_bendTimer > 0)
        {
            transform.Rotate(new Vector3(0f, 0f, GameManager.Instance.LastCrisperPosition * 2f) * Time.deltaTime);
            transform.Translate(Vector3.down * Time.deltaTime * 0.15f); //если я добавлю спец.предметы, то 0.15 - это будет вес предмета, условно все контейнеры так весят
            _previousRot = transform.rotation.z;
            _bendTimer -= Time.deltaTime;
        }
        else
        {
            if(_afterBendTimer > 0)
            {
                transform.Translate(0f, 0.003f, 0f * Time.deltaTime);
                transform.Rotate(new Vector3(0f, 0f, _previousRot) * Time.deltaTime);
                _afterBendTimer -= Time.deltaTime;
            }    
        }
    }

    public void MakeBend()
    {
        _bendTimer = 1f;
        _afterBendTimer = 0.4f;
        _isShouldBend = true;
    }

    public void SunkBarge()
    {
        if (transform.rotation.z > 0.15f || transform.rotation.z < -0.15f && GameManager.Instance.IsWinned)
        {
            GameManager.Instance.IsLost = true;
            transform.Translate(Vector3.down * Time.deltaTime);
        }
    }
}
