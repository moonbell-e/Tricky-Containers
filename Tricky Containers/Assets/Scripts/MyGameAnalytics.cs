using GameAnalyticsSDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameAnalytics : MonoBehaviour
{
    private static int _currentLevel;

    private void Start()
    {
        GameAnalytics.Initialize();
    }

    public static void OnLevelStart(int level = 1)
    {
        _currentLevel = level;
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, $"level{level}");
    }

    public static void OnLevelCompleted()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, $"level{_currentLevel}");
    }

    public static void OnLevelFailed()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, $"level{_currentLevel}");
    }
}
