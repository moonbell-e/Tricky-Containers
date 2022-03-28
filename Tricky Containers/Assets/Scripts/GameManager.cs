using UnityEngine;
using TMPro;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BlockMoveSystem _blockMoveSystem;
    [SerializeField] public float LastCrisperPosition = 0f;
    [SerializeField] private CrisperSpawner _crisperSpawner;
    [SerializeField] private GameObject _crisperLandEffect;
    [SerializeField] private TMP_Text _crisperCounterText;
    [SerializeField] private GameObject _winUI;
    [SerializeField] private GameObject _loseUI;
    [SerializeField] private Animator _waveAnimaton;
    [SerializeField] private Animator _counterAnimaton;
    [SerializeField] private Transform _crispersOnBarge;


    public bool IsWinned;
    public bool IsLost;
    [SerializeField] private BargeEntity _bargeEntity;

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
        if (IsWinned && !IsLost)
        {
            _winUI.SetActive(true);
            OnEvent_CrisperHitFinishLine();
            _bargeEntity.gameObject.transform.Translate(Vector3.left * Time.deltaTime * 2f);
            _bargeEntity.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0f), Time.deltaTime);
        }
        if (IsLost && !IsWinned)
        {
            _loseUI.SetActive(true);
            _blockMoveSystem.enabled = false;
            _waveAnimaton.Play("WaveAnimationClip");
            _bargeEntity.gameObject.transform.Translate(Vector3.down * Time.deltaTime * 0.5f);
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
            if (collision.gameObject.CompareTag("Tractor"))
                IsLost = true;
            OnEvent_CrisperHitCrisper(crisper, crisperEntity);
            if (crisper.transform.position.y > 6.5f)
            {
                OnEvent_CrisperHitFinishLine();
            }
        }
    }

    private void OnEvent_CrisperHitFinishLine()
    {
        IsWinned = true;
        _blockMoveSystem.enabled = false;
    }

    private void OnEvent_CrisperHitBarge(CrisperEntity crisper, BargeEntity bargeEntity)
    {
        //crisper.GetComponent<Rigidbody>().useGravity = true;
        crisper.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
        GetLandParticles(_crisperLandEffect, crisper).Play();
        SpawnNewCrisper();
        _crisperSpawner.LastSpawnedCrisper.transform.SetParent(_crispersOnBarge);
        _waveAnimaton.Play("WaveAnimationClip", -1, 0f);
        BendBarge(bargeEntity);
        CrisperActions(crisper);
        Shaker.Instance.ShakeCamera();
    }

    private void OnEvent_CrisperHitCrisper(CrisperEntity crisper, CrisperEntity hittedCrisper)
    {
        //crisper.GetComponent<Rigidbody>().useGravity = true;
        crisper.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
        hittedCrisper.enabled = false;
        GetLandParticles(_crisperLandEffect, crisper).Play();
        SpawnNewCrisper();
        _waveAnimaton.Play("WaveAnimationClip", -1, 0f);
        BendBargeTakeCrispers();
        CrisperActions(crisper);
        Shaker.Instance.ShakeCamera();

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

    private void OnEventCrisperCount(int newCount)
    {
        _crisperCounterText.text = newCount.ToString();
        _counterAnimaton.Play("CounterAnimationClip", -1, 0f);
    }

    private ParticleSystem GetLandParticles(GameObject crisperLandEffect, CrisperEntity crisper)
    {
        crisperLandEffect.transform.position = crisper.transform.position;
        return crisperLandEffect.GetComponent<ParticleSystem>();
    }

    private void SpawnNewCrisper()
    {
        if (!IsWinned || (!IsLost))
            _crisperSpawner.SpawnRandom();
    }

    private void CrisperActions(CrisperEntity crisper)
    {
        _blockMoveSystem.CurrentCrisper = _crisperSpawner.CrisperEntity;
        crisper.ShowLightning(false);
        LastCrisperPosition = -crisper.transform.position.x;
    }

    private void BendBarge(BargeEntity bargeEntity)
    {
        _bargeEntity = bargeEntity;
        bargeEntity.MakeBend();
    }
    private void BendBargeTakeCrispers()
    {
        _crisperSpawner.LastSpawnedCrisper.transform.SetParent(_crispersOnBarge);
        _bargeEntity.MakeBend();
    }
    private void Initialize()
    {
        enabled = true;
    }
}
