using System.Collections;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private Transform _camera;

    [SerializeField] private float _cameraShakeDuration = 0.5f;
    [SerializeField] private float _cameraShakeAmplitude = 1f;

    private void Awake()
    {
        _camera = GetComponent<Transform>();
    }

    public void ShakeCamera()
    {
        StartCoroutine(ShakeCameraCoroutine());
    }
    private IEnumerator ShakeCameraCoroutine()
    {
        var originalPos = _camera.localPosition;
        var shakeTime = _cameraShakeDuration + Time.time;
        while (shakeTime > Time.time)
        {
            _camera.localPosition = originalPos + Random.insideUnitSphere * _cameraShakeAmplitude;
            yield return null;
        }
        _camera.localPosition = originalPos;
    }
}
