using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;
using System.Collections;

public class MainUI : MonoBehaviour
{
    [Header("UI Panels")]
    [Space]
    [SerializeField] private MainLoaderUI mainLoaderUI;
    [SerializeField] private MainScreenUI mainScreenUI;
    [SerializeField] private SettingsUI settingsUI;
    [SerializeField] private SelectModeUI selectModeUI;
    [SerializeField] private SelectCharacterUI selectCharacterUI;
    [SerializeField] private ChooseGameLevelUI chooseGameLevelPanel_Puzzle;
    [SerializeField] private ChooseGameLevelUI chooseGameLevelPanel_MiniGame;
    [SerializeField] private MiniGameUI miniGameUI;
    [Header("Transitions References")]
    [Space]
    [SerializeField] private Transform leftBottomTransitionPanel;
    [SerializeField] private Transform rightTopTransitionPanel;
    [SerializeField] private Transform centerTransitionPanel;
    [Header("Transitions Data")]
    [Space]
    [SerializeField] private float transitionDuration = 1f;
    [SerializeField] private float transitionDelay = 0.1f;

    private List<MainCanvasPanel> panelsList = new List<MainCanvasPanel>();

    private CurrentGameManager _currentGameManager;

    private Vector3 leftBottomTransitionPanelStartPosition = new Vector3(0f, 0f, 0f);
    private Vector3 rightTopTransitionPanelStartPosition = new Vector3(0f, 0f, 0f);

    private void Awake()
    {
        FillPanelsList();

        leftBottomTransitionPanelStartPosition = leftBottomTransitionPanel.transform.position;
        rightTopTransitionPanelStartPosition = rightTopTransitionPanel.transform.position;
    }

    private void Start()
    {
        SetStartSettings();
    }

    #region Zenject
    [Inject]
    private void Construct(CurrentGameManager currentGameManager)
    {
        _currentGameManager = currentGameManager;
    }
    #endregion Zenject

    #region Buttons Methods
    public void ShowMainScreenUI()
    {
        ActivateMainCanvasPanel(UIPanels.MainScreenPanel);
        mainScreenUI.ActivateIdleAnimation();
    }

    public void ShowSettingsUI()
    {
        ActivateMainCanvasPanel(UIPanels.SettingsPanel);
        mainScreenUI.StopIdleAnimation();
    }

    public void ShowSelectGameModeUI()
    {
        ActivateMainCanvasPanel(UIPanels.SelectModePanel);
        mainScreenUI.StopIdleAnimation();
    }

    public void ShowSelectCharacterUI()
    {
        ActivateMainCanvasPanel(UIPanels.SelectCharacterPanel);
    }

    public void ShowSelectGameLevel()
    {
        if(_currentGameManager.CurrentGameType == GameLevelTypes.Puzzle)
        {
            ActivateMainCanvasPanel(UIPanels.SelectGameLevel_Puzzle);
        }
        else if(_currentGameManager.CurrentGameType == GameLevelTypes.MiniGame)
        {
            ActivateMainCanvasPanel(UIPanels.SelectGameLevel_MiniGame);
        }
    }

    public void ShowGameLevelUI()
    {
        ActivateMainCanvasPanel(UIPanels.MiniGamePanel);
    }
    #endregion Buttons Methods

    [ContextMenu("Show Transition")]
    public void StartTransitionAnimation()
    {
        StartCoroutine(MakeScreenTransitionCoroutine());
    }

    private void FillPanelsList()
    {
        panelsList.Add(mainLoaderUI);
        panelsList.Add(mainScreenUI);
        panelsList.Add(settingsUI);
        panelsList.Add(selectModeUI);
        panelsList.Add(selectCharacterUI);
        panelsList.Add(chooseGameLevelPanel_Puzzle);
        panelsList.Add(chooseGameLevelPanel_MiniGame);
        panelsList.Add(miniGameUI);
    }

    private void SetStartSettings()
    {
        ActivateMainCanvasPanel(UIPanels.MainLoaderPanel);
        mainLoaderUI.StartLoadingAnimation(OnLoadingAnimationFinishedCallback);
    }

    private void ActivateMainCanvasPanel(UIPanels panel)
    {
        //StartCoroutine(MakeScreenTransitionCoroutine(panel));
        for (int i = 0; i < panelsList.Count; i++)
        {
            if (panelsList[i].PanelType == panel)
            {
                panelsList[i].ShowPanel();
            }
            else
            {
                panelsList[i].HidePanel();
            }
        }
    }

    private void ChangeCanvasPanel(UIPanels panel)
    {
        for (int i = 0; i < panelsList.Count; i++)
        {
            if (panelsList[i].PanelType == panel)
            {
                panelsList[i].ShowPanel();
            }
            else
            {
                panelsList[i].HidePanel();
            }
        }
    }

    private void OnLoadingAnimationFinishedCallback()
    {
        ShowMainScreenUI();
        mainLoaderUI.ResetUIData();
    }

    private void HideMainUI()
    {
        for (int i = 0; i < panelsList.Count; i++)
        {
            Debug.Log($"Hide {panelsList[i]}");
            panelsList[i].HidePanel();
        }
    }

    private IEnumerator MakeScreenTransitionCoroutine(/*UIPanels panel*/)
    {
        yield return null;
        leftBottomTransitionPanel.DOMove(centerTransitionPanel.position, transitionDuration);
        rightTopTransitionPanel.DOMove(centerTransitionPanel.position, transitionDuration).OnComplete(() =>
        {
            //ChangeCanvasPanel(panel);
            StartCoroutine(FinisheTransitionCoroutine());
        });
        //yield return new WaitForSeconds(transitionDelay);
        //leftBottomTransitionPanel.DOMove(Vector3.zero, transitionDuration);
        //rightTopTransitionPanel.DOMove(Vector3.zero, transitionDuration).OnComplete(() =>
        //{

        //});


    }

    private IEnumerator FinisheTransitionCoroutine()
    {
        yield return new WaitForSeconds(transitionDelay);
        leftBottomTransitionPanel.DOMove(leftBottomTransitionPanelStartPosition, transitionDuration);
        rightTopTransitionPanel.DOMove(rightTopTransitionPanelStartPosition, transitionDuration);
    }
}
