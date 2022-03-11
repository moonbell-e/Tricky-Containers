using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Barges", menuName = "Scriptable Objects/New Barge")]
public class BargeData : ScriptableObject
{
    [Header("Barge settings")]

    [SerializeField] private GameObject _bargePrefab;
    private enum BargeType
    {
        FirstType,
        SecondType,
        ThirdType
    };

    [SerializeField] private BargeType _type;
}
