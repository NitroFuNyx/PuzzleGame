using System;

namespace Localization
{
    [Serializable]
    public class LanguageTextsHolder
    {
        public Data data;
    }

    [Serializable]
    public class Data
    {
        public MainScreenUIData mainscreenUITexts;
        public SettingsUIData settingsUITexts;
        public SelectGameModeUIData selectGameModeUITexts;
        public SelectCharacterUIData selectCharacterUITexts;
        public ChoosePuzzleLevelUIData choosePuzzleLevelUITexts;
        public ChooseMiniGameLevelUIData chooseMiniGameLevelUITexts;
        public miniGameUIData miniGameUITexts;
        public puzzleUIData puzzleUITexts;
        public pauseUIData pauseUITexts;
    }

    [Serializable]
    public class MainScreenUIData
    {
        public string titleText;
    }

    [Serializable]
    public class SettingsUIData
    {
        public MainSettingsPanelData mainSettingsPanel;
        public ChangeLanguagePanelData changeLanguagePanel;
        public InfoPanelData infoPanel;
    }

    [Serializable]
    public class MainSettingsPanelData
    {
        public string uiTitleText;
        public string soundsButtonText;
        public string languageButtonText;
        public string infoButtonText;
    }

    [Serializable]
    public class ChangeLanguagePanelData
    {
        public string uiTitleText;
    }

    [Serializable]
    public class InfoPanelData
    {
        public string uiTitleText;
        public string infoText;
        public string privacyPolicyTitleText;
    }

    [Serializable]
    public class SelectGameModeUIData
    {
        public string uiTitleText;
        public string puzzlePanelText;
        public string miniGamePanelText;
    }

    [Serializable]
    public class SelectCharacterUIData
    {
        public string uiTitleText;
    }

    [Serializable]
    public class ChoosePuzzleLevelUIData
    {
        public string uiTitleText;
        public string startText;
        public string bestTimeText;
        public string currentTimeText;
    }

    [Serializable]
    public class ChooseMiniGameLevelUIData
    {
        public string uiTitleText;
        public string startText;
        public string bestScore;
    }

    [Serializable]
    public class miniGameUIData
    {
        public string uiTitleText;
        public string videoText;
        public string gamestartsText;
        
    }

    [Serializable]
    public class puzzleUIData
    {
        public string uiTitleText;
        public string inJustText;
        public string finishTimeText;
        public string playAgainButtonText;
    }

    [Serializable]
    public class pauseUIData
    {
        public string uiTitleText;
        public string exitText;
        public string continueText;
    }
}
