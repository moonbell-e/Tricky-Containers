using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Crispers", menuName = "Scriptable Objects/New Crisper")]
public class CrisperData : ScriptableObject
{
    [SerializeField] private GameObject _crisperPrefab;

    private enum CrisperType
    {
        FirstType,
        SecondType,
        ThirdType
    };

    [SerializeField] private CrisperType _type;
}
