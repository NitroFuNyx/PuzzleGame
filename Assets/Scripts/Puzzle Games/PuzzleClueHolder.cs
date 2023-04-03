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

    public int ClueIndex { get => clueIndex; }

    public void ShowClue()
    {
        //if(ClueIndex == index)
        //{
            clueVFX.Play();
        //}
    }

    public void HideClue()
    {
        //if (ClueIndex == index)
        //{
            clueVFX.Stop();
        //}
    }
}
