using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Crisper", menuName = "Scriptable Objects/New Crisper")]
public partial class CrisperData : ScriptableObject
{
    [SerializeField] private GameObject _crisperPrefab;

    [SerializeField] private CrisperType _type;
    public GameObject CrisperPrefab => _crisperPrefab;
    public CrisperType CrisperType => _type;
}
