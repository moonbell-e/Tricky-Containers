using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Barge", menuName = "Scriptable Objects/New Barge")]
public partial class BargeData : ScriptableObject
{
    [Header("Barge settings")]

    [SerializeField] private GameObject _bargePrefab;

    [SerializeField] private BargeType _type;
}
