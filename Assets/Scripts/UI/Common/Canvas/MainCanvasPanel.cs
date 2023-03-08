using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvasPanel : PanelActivationManager
{
    [Header("Panel Data")]
    [Space]
    [SerializeField] private UIPanels panelType;

    public UIPanels PanelType { get => panelType; set => panelType = value; }
}
