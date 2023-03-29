using UnityEngine;
using DG.Tweening;

public class PopItButton : ButtonInteractionHandler
{
    [Header("Button Data")]
    [Space]
    [SerializeField] private bool isInCorrectPos = false;
    [Header("Sprites")]
    [Space]
    [SerializeField] private Sprite pressedButtonSprite;
    [SerializeField] private Sprite releasedButtonSprite;
    [Header("Durations")]
    [Space]
    [SerializeField] private float changeAlphaDuration = 0.2f;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        ChangeButtonAlpha();
    }

    public override void ButtonActivated()
    {
        isInCorrectPos = !isInCorrectPos;
        ChangeButtonAlpha();
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
