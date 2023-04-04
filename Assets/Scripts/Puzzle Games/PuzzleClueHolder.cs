using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleClueHolder : MonoBehaviour
{
    [Header("Item Data")]
    [Space]
    [SerializeField] private int clueIndex = 0;
    [Header("Clue Data")]
    [Space]
    [SerializeField] private ParticleSystem clueVFX;

    private bool isActive = false;

    public int ClueIndex { get => clueIndex; }
    public bool IsActive { get => isActive; private set => isActive = value; }

    public void ShowClue()
    {
        isActive = true;
        clueVFX.Play();
    }

    public void HideClue()
    {
        isActive = false;
        clueVFX.Stop();
    }
}
