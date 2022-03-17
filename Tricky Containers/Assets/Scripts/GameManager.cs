using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BlockMoveSystem _blockMoveSystem;
    [SerializeField] public float _lastCrisperPosition = 0f;

    private void OnEnable()
    {
        CrisperEntity.OnHitCollider += OnEvent_CrisperHitCollider;
    }

    private void OnDisable()
    {
        CrisperEntity.OnHitCollider -= OnEvent_CrisperHitCollider;
    }

    private void OnEvent_CrisperHitCollider(CrisperEntity crisper, Collision collision)
    {
        if (collision.gameObject.TryGetComponent<BargeEntity>(out var barge)) 
        {
            OnEvent_CrisperHitBarge(crisper, barge);
        }
    }

    private void OnEvent_CrisperHitBarge(CrisperEntity crisper, BargeEntity bargeEntity)
    {
        _blockMoveSystem.DisablePlayerInput();
        crisper.ShowLightning(false);
        Shaker.Instance.ShakeCamera();
        _lastCrisperPosition = -crisper.transform.position.x;
        bargeEntity.MakeBend();
    }
}
