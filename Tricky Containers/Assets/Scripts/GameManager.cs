using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BlockMoveSystem _blockMoveSystem;
    [SerializeField] public float LastCrisperPosition = 0f;
    [SerializeField] private CrisperSpawner _crisperSpawner;
    [SerializeField] private GameObject _crisperLandEffect;
    [SerializeField] private TMP_Text _crisperCounterText;
    [SerializeField] private GameObject _winUI;
    [SerializeField] private GameObject _loseUI;


    private bool _isWinned;
    public bool IsLost;
    private BargeEntity _bargeEntity;

    #region Singleton Init
    private static GameManager _instance;
    private void Awake() // Init in order
    {
        if (_instance == null)
            Init();
        else if (_instance != this)
        {
            Debug.Log($"Destroying {gameObject.name}, caused by one singleton instance");
            Destroy(gameObject);
        }
    }
    public static GameManager Instance // Init not in order
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
        _instance = FindObjectOfType<GameManager>();
        if (_instance != null)
            _instance.Initialize();
    }
    #endregion
    private void OnEnable()
    {
        CrisperSpawner.OnCrisperSpawnedEvent += OnEvent_OnCrisperSpawned;
        CrisperSpawner.OnCrisperCountEvent += OnEventCrisperCount;
        BargeSpawner.OnBargeSpawnedEvent += OnEvent_OnBargeSpawned;
        CrisperEntity.OnHitCollider += OnEvent_CrisperHitCollider;
    }

    private void OnDisable()
    {
        CrisperEntity.OnHitCollider -= OnEvent_CrisperHitCollider;
        CrisperSpawner.OnCrisperSpawnedEvent -= OnEvent_OnCrisperSpawned;
        CrisperSpawner.OnCrisperCountEvent -= OnEventCrisperCount;
        BargeSpawner.OnBargeSpawnedEvent -= OnEvent_OnBargeSpawned;
    }

    private void Update()
    {
        if (_isWinned)
        {
            _winUI.SetActive(true);
            _bargeEntity.gameObject.transform.Translate(Vector3.left * Time.deltaTime * 5f);
        }

        if (IsLost)
        { 
            _loseUI.SetActive(true);
            _blockMoveSystem.enabled = false;
            _bargeEntity.gameObject.transform.Translate(Vector3.down * Time.deltaTime);
        }
    }

    private void OnEvent_CrisperHitCollider(CrisperEntity crisper, Collision collision)
    {
        if (collision.gameObject.TryGetComponent<BargeEntity>(out var barge))
        {
            OnEvent_CrisperHitBarge(crisper, barge);
        }
        else if (collision.gameObject.TryGetComponent<CrisperEntity>(out var crisperEntity))
        {
            OnEvent_CrisperHitCrisper(crisper, crisperEntity);
        }
        else if(collision.gameObject.TryGetComponent<FinishLineEntity>(out var finishLineEntity))
        {
            OnEvent_CrisperHitFinishLine();
        }
    }

    private void OnEvent_CrisperHitFinishLine()
    {
        _isWinned = true;
        _blockMoveSystem.enabled = false;
    }

    private void OnEvent_CrisperHitBarge(CrisperEntity crisper, BargeEntity bargeEntity)
    {
        GetLandParticles(_crisperLandEffect, crisper).Play();
        SpawnNewCrisper();
        _blockMoveSystem.CurrentCrisper = _crisperSpawner.CrisperEntity;
        crisper.ShowLightning(false);
        Shaker.Instance.ShakeCamera();
        LastCrisperPosition = -crisper.transform.position.x;
        _bargeEntity = bargeEntity;
        _bargeEntity.MakeBend();
    }

    private void OnEvent_CrisperHitCrisper(CrisperEntity crisper,  CrisperEntity hittedCrisper)
    {
        hittedCrisper.enabled = false;
        GetLandParticles(_crisperLandEffect, crisper).Play();
        SpawnNewCrisper();
        _blockMoveSystem.CurrentCrisper = _crisperSpawner.CrisperEntity;
        crisper.ShowLightning(false);
        Shaker.Instance.ShakeCamera();
        LastCrisperPosition = -crisper.transform.position.x;
        _bargeEntity.MakeBend();
    }

    private void OnEvent_OnCrisperSpawned(CrisperSpawner crisperSpawner)
    {
        _crisperSpawner = crisperSpawner;
        crisperSpawner.SpawnRandom();
        _blockMoveSystem.CurrentCrisper = crisperSpawner.CrisperEntity;
    }

    private void OnEvent_OnBargeSpawned(BargeEntity barge, BargeSpawner bargeSpawner)
    {
        Debug.Log(barge.name);
    }

    private void OnEventCrisperCount(int newCount) => _crisperCounterText.text = newCount.ToString();

    private ParticleSystem GetLandParticles(GameObject crisperLandEffect, CrisperEntity crisper)
    {
        crisperLandEffect.transform.position = crisper.transform.position;
        return crisperLandEffect.GetComponent<ParticleSystem>();
    }

    private void SpawnNewCrisper()
    {
        if (!_isWinned || (!IsLost))
            _crisperSpawner.SpawnRandom();
    }

    private void Initialize()
    {
        enabled = true;
    }
}
