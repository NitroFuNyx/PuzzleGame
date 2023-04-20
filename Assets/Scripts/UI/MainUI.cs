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
    //[SerializeField] private ChooseGameLevelUI chooseGameLevelPanel_Puzzle;
    //[SerializeField] private ChooseGameLevelUI chooseGameLevelPanel_MiniGame;
    [SerializeField] private PuzzleGameUI puzzleGameUI;
    [SerializeField] private MiniGameUI miniGameUI;
    [SerializeField] private PauseUI pauseUI;
    //[SerializeField] private PanelActivationManager transitionPanel;
    [Header("Transitions References")]
    [Space]
    [SerializeField] private Transform leftBottomTransitionPanel;
    [SerializeField] private Transform rightTopTransitionPanel;
    [SerializeField] private Transform centerTransitionPanel;
    [SerializeField] private Transform rightTransitionPanelStartPivot;
    [SerializeField] private Transform leftTransitionPanelStartPivot;
    [Header("Transitions Data")]
    [Space]
    [SerializeField] private float transitionDuration = 1f;
    [SerializeField] private float transitionDelay = 0.1f;
    [SerializeField] private Vector3 leftBottomTransitionPanelStartPosition;
    [SerializeField] private Vector3 rightTopTransitionPanelStartPosition;

    private List<MainCanvasPanel> panelsList = new List<MainCanvasPanel>();

    private CurrentGameManager _currentGameManager;
    private SystemTimeManager _systemTimeManager;
    private AudioManager _audioManager;

    private void Awake()
    {
        FillPanelsList();
        //transitionPanel.HidePanel();
    }

    private void Start()
    {
        SetStartSettings();
    }

    #region Zenject
    [Inject]
    private void Construct(CurrentGameManager currentGameManager, SystemTimeManager systemTimeManager, AudioManager audioManager)
    {
        _currentGameManager = currentGameManager;
        _systemTimeManager = systemTimeManager;
        _audioManager = audioManager;
    }
    #endregion Zenject

    #region Buttons Methods
    public void ShowMainScreenUI()
    {
        ActivateMainCanvasPanel(UIPanels.MainScreenPanel);
        mainScreenUI.ActivateIdleAnimation();
        _audioManager.PlayMusic_MainUI();
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

    public void GameModeDefined_ExecuteReaction()
    {
        if(_currentGameManager.CurrentGameType == GameLevelTypes.MiniGame)
        {
            Debug.Log($"Mini Game End {gameObject}");
            ShowSelectCharacterUI();
        }
        else
        {
            ShowSelectGameLevel();
        }
    }

    public void ShowSelectGameLevel()
    {
        _audioManager.PlayMusic_MainUI();
        if(_currentGameManager.CurrentGameType == GameLevelTypes.Puzzle)
        {
            ActivateMainCanvasPanel(UIPanels.SelectModePanel);
            puzzleGameUI.ShowMainModePanel();
        }
        else if(_currentGameManager.CurrentGameType == GameLevelTypes.MiniGame)
        {
            ActivateMainCanvasPanel(UIPanels.SelectModePanel);
            miniGameUI.HideGameFinishedPanel();
        }
    }

    public void ShowGameLevelUI()
    {
        if(_currentGameManager.CurrentGameType == GameLevelTypes.MiniGame)
        {
            ActivateMainCanvasPanel(UIPanels.MiniGamePanel);
            _audioManager.PlayMusic_MiniGame();
        }
        else
        {
            ActivateMainCanvasPanel(UIPanels.PuzzleGamePanel);
            _audioManager.PlayMusic_Puzzle();
        }
    }

    public void ShowPauseUI()
    {
        _systemTimeManager.PauseGame();
        ActivateMainCanvasPanel(UIPanels.PausePanel);
    }

    public void HidePauseUI()
    {
        _systemTimeManager.ResumeGame();

        if(_currentGameManager.CurrentGameType == GameLevelTypes.MiniGame)
        {
            ActivateMainCanvasPanel(UIPanels.MiniGamePanel);
        }
        else
        {
            ActivateMainCanvasPanel(UIPanels.PuzzleGamePanel);
        }
    }

    public void ExitGameMode()
    {
        _systemTimeManager.ResumeGame();
        _audioManager.PlayMusic_MainUI();

        if (_currentGameManager.CurrentGameType == GameLevelTypes.MiniGame)
        {
            ShowSelectGameLevel();
        }
        else
        {
            ShowSelectGameModeUI();
        } 
    }
    #endregion Buttons Methods

    [ContextMenu("Show Transition")]
    public void StartTransitionAnimation(UIPanels panel)
    {
        //transitionPanel.ShowPanel();
        StartCoroutine(MakeScreenTransitionCoroutine(panel));
    }

    private void FillPanelsList()
    {
        panelsList.Add(mainLoaderUI);
        panelsList.Add(mainScreenUI);
        panelsList.Add(settingsUI);
        panelsList.Add(selectModeUI);
        panelsList.Add(selectCharacterUI);
        //panelsList.Add(chooseGameLevelPanel_Puzzle);
        //panelsList.Add(chooseGameLevelPanel_MiniGame);
        panelsList.Add(puzzleGameUI);
        panelsList.Add(miniGameUI);
        panelsList.Add(pauseUI);
    }

    private void SetStartSettings()
    {
        ActivateMainCanvasPanel(UIPanels.MainLoaderPanel);
        mainLoaderUI.StartLoadingAnimation(OnLoadingAnimationFinishedCallback);
    }

    private void ActivateMainCanvasPanel(UIPanels panel)
    {
        //if(panel != UIPanels.MainLoaderPanel && panel != UIPanels.MainScreenPanel)
        //{
        //    StartTransitionAnimation(panel);
        //}
        //else
        //{
            if (panel == UIPanels.SelectModePanel)
            {
                selectModeUI.StartPanelsAnimations();
            }
            else
            {
                selectModeUI.StopPanelsAnimations();
            }
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
        //}
    }

    private void HideMainCanvasPanel(UIPanels panel)
    {
        //StartCoroutine(MakeScreenTransitionCoroutine(panel));
        for (int i = 0; i < panelsList.Count; i++)
        {
            if (panelsList[i].PanelType == panel)
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

    private IEnumerator MakeScreenTransitionCoroutine(UIPanels panel)
    {
        yield return null;
        leftBottomTransitionPanel.DOMove(centerTransitionPanel.position, transitionDuration);
        rightTopTransitionPanel.DOMove(centerTransitionPanel.position, transitionDuration).OnComplete(() =>
        {
            if (panel == UIPanels.SelectModePanel)
            {
                selectModeUI.StartPanelsAnimations();
            }
            else
            {
                selectModeUI.StopPanelsAnimations();
            }
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
            StartCoroutine(FinisheTransitionCoroutine());
        });
    }

    private IEnumerator FinisheTransitionCoroutine()
    {
        yield return new WaitForSeconds(transitionDelay);
        leftBottomTransitionPanel.DOMove(leftTransitionPanelStartPivot.position, transitionDuration);
        rightTopTransitionPanel.DOMove(rightTransitionPanelStartPivot.position, transitionDuration).OnComplete(() =>
        {
            //transitionPanel.HidePanel();
        });
    }
}
