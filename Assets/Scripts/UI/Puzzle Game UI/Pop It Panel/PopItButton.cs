using UnityEngine;
using DG.Tweening;

public class PopItButton : ButtonInteractionHandler
{
    [Header("Button Data")]
    [Space]
    [SerializeField] private bool isInCorrectPos = false;
    [Header("Durations")]
    [Space]
    [SerializeField] private float changeAlphaDuration = 0.2f;

    private PopItGameStateManager _popItGameStateManager;

    public bool IsInCorrectPos { get => isInCorrectPos; private set => isInCorrectPos = value; }

    private void Start()
    {
        ChangeButtonAlpha();
    }

    public override void ButtonActivated()
    {
        if(!_popItGameStateManager.Finished)
        {
            isInCorrectPos = !isInCorrectPos;
            ChangeButtonAlpha();
            _popItGameStateManager.CheckButtonsState();
        }
    }

    public void CashComponents(PopItGameStateManager popItGameStateManager)
    {
        _popItGameStateManager = popItGameStateManager;
    }

    private void ChangeButtonAlpha()
    {
        float alpha = 1f;

        if(isInCorrectPos)
        {
            alpha = 0f;
        }

        buttonImage.DOFade(alpha, changeAlphaDuration);
    }
}
