using System.Collections.Generic;
using UnityEngine;
using Zenject;

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

    private List<MainCanvasPanel> panelsList = new List<MainCanvasPanel>();

    private CurrentGameManager _currentGameManager;

    private void Awake()
    {
        FillPanelsList();
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

    }
    #endregion Buttons Methods

    private void FillPanelsList()
    {
        panelsList.Add(mainLoaderUI);
        panelsList.Add(mainScreenUI);
        panelsList.Add(settingsUI);
        panelsList.Add(selectModeUI);
        panelsList.Add(selectCharacterUI);
        panelsList.Add(chooseGameLevelPanel_Puzzle);
        panelsList.Add(chooseGameLevelPanel_MiniGame);
    }

    private void SetStartSettings()
    {
        ActivateMainCanvasPanel(UIPanels.MainLoaderPanel);
        mainLoaderUI.StartLoadingAnimation(OnLoadingAnimationFinishedCallback);
    }

    private void ActivateMainCanvasPanel(UIPanels panel)
    {
        for(int i = 0; i < panelsList.Count; i++)
        {
            if(panelsList[i].PanelType == panel)
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
}
