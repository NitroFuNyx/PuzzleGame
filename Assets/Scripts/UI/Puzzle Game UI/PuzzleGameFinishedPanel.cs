using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGameFinishedPanel : MonoBehaviour
{
    private PanelActivationManager activationManager;

    private void Awake()
    {
        activationManager = GetComponent<PanelActivationManager>();
    }

    private void Start()
    {
        activationManager.HidePanel();
    }
}
