using UnityEngine;
using UnityEngine.UI;
using Zenject;
using DG.Tweening;
using TMPro;

public class ChooseGameLevelButton : ButtonInteractionHandler
{
    [Header("Button Data")]
    [Space]
    [SerializeField] private GameLevelTypes gameType;
    [SerializeField] private GameLevelStates levelState;
    [SerializeField] private int levelIndex;
    [Header("Internal References")]
    [Space]
    [SerializeField] private Image lockImage;
    [SerializeField] private TextMeshProUGUI costAmountText;
    [Header("Lock Rotation Data")]
    [Space]
    [SerializeField] private Vector3 rotationPunchVector = new Vector3(1f, 1f, 1f);
    [SerializeField] private float punchDuration = 1f;
    [SerializeField] private int punchFreequency = 3;
    [Header("Cost Text Data")]
    [Space]
    [SerializeField] private Vector3 costTextPunchVector = new Vector3(0.2f, 0.2f, 0.2f);
    [SerializeField] private int costTextPunchScaleFreequency = 4;
    [SerializeField] private Color costTextBlockedColor;
    

    private MainUI _mainUI;
    private CurrentGameManager _currentGameManager;

    private bool isAnimationInProcess = false;

    #region Zenject
    [Inject]
    private void Construct(MainUI mainUI, CurrentGameManager currentGameManager)
    {
        _mainUI = mainUI;
        _currentGameManager = currentGameManager;
    }
    #endregion Zenject

    public override void ButtonActivated()
    {
        if(levelState != GameLevelStates.Locked)
        {
            ShowAnimation_ButtonPressed();
            _currentGameManager.ActivateGameLevelEnvironment(levelIndex);
            StartCoroutine(ActivateDelayedButtonMethodCoroutine(_mainUI.ShowGameLevelUI));
        }
        else
        {
            if(!isAnimationInProcess)
            {
                isAnimationInProcess = true;
                lockImage.transform.DOKill();
                costAmountText.DOKill();
                costAmountText.transform.DOKill();
                costAmountText.DOColor(costTextBlockedColor, punchDuration / 2).OnComplete(() =>
                {
                    costAmountText.transform.DOScale(Vector3.one, punchDuration / 2);
                    costAmountText.DOColor(Color.white, punchDuration);
                });
                costAmountText.transform.DOPunchScale(costTextPunchVector, punchDuration, costTextPunchScaleFreequency);
                lockImage.transform.DOPunchRotation(rotationPunchVector, punchDuration, punchFreequency).OnComplete(() =>
                {
                    lockImage.transform.DORotate(Vector3.zero, punchDuration);
                    isAnimationInProcess = false;
                });
            }
        }
    }

    public void SetButtonData(GameLevelStates gameLevelState, GameLevelTypes gameLevelType, int index)
    {
        levelState = gameLevelState;
        gameType = gameLevelType;
        levelIndex = index;
    }
}
