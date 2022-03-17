using System.Collections;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private float _cameraShakeDuration = 0.3f;
    [SerializeField] private float _cameraShakeAmplitude = 0.1f;
    [SerializeField] private bool _isAttenuating;

    #region Singleton Init
    private static Shaker _instance;
    private void Awake() // Init in order
    {
        if (_instance == null)
            Init();
        else if (_instance != this)
        {
            Debug.Log($"Destroying {gameObject.name}, caused by one singleton instance");
            Destroy(gameObject);
        }
        _camera = GetComponent<Transform>();
    }
    public static Shaker Instance // Init not in order
    {
        get
        {
            if (_instance == null)
                Init();
            return _instance;
        }
        private set { _instance = value; }
    }
    static void Init() // Init script
    {
        _instance = FindObjectOfType<Shaker>();
        if (_instance != null)
            _instance.Initialize();
    }
    #endregion
    public void ShakeCamera()
    {
        StartCoroutine(ShakeCameraCoroutine());
    }
    private IEnumerator ShakeCameraCoroutine()
    {
        var originalPos = _camera.localPosition;
        var shakeTime = _cameraShakeDuration + Time.time;
        if (_isAttenuating)
        {
            while (shakeTime > Time.time)
            {
                _camera.localPosition = originalPos + Random.insideUnitSphere * _cameraShakeAmplitude;
                _cameraShakeAmplitude -= _cameraShakeAmplitude / 20;
                yield return null;
            }
            _camera.localPosition = originalPos;
        }
        else
        {
            while (shakeTime > Time.time)
            {
                _camera.localPosition = originalPos + Random.insideUnitSphere * _cameraShakeAmplitude;
                yield return null;
            }
            _camera.localPosition = originalPos;
        }
    }

    private void Initialize()
    {
        enabled = true;
    }
}
