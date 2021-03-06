﻿using System.Collections;
using UnityEngine;

public class LevelStateManager : MonoBehaviour
{
    #region Singleton
    public static LevelStateManager Instance;
    private void OnEnable () => Instance = this;
    #endregion

    /* Temp Dev work: Move one unit by clicking on a node */

    [Header ("Dependencies")]
    public Camera gameCamera;
    public GridSystem generatedGrid;

    /* Level Initialization */

    private void Start()
    {
        InitializeLevel();
    }

    private void InitializeLevel()
    {
        HandCardManager.Instance.InitializeHandForLevel();
    }
}