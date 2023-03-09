using UnityEngine;

public class MainScreenUI : MainCanvasPanel
{
    [Header("Texts")]
    [Space]
    [SerializeField] private MainScreenUI_ActionTextIdleAnimator textAnimator;

    public void ActivateIdleAnimation()
    {
        textAnimator.StartScaleAnimation();
    }

    public void StopIdleAnimation()
    {
        textAnimator.StopScaleAnimation();
    }
}
