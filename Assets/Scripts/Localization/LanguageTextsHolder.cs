using System;

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
    public ChoosePuzzleLevelUIData choosePuzzleLevelUITexts;
    public ChooseMiniGameLevelUIData chooseMiniGameLevelUITexts;
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
public class SelectGameModeUIData
{
    public string uiTitleText;
    public string puzzlePanelText;
    public string miniGamePanelText;
}

[Serializable]
public class ChoosePuzzleLevelUIData
{
    public string uiTitleText;
    public string bestTimeText;
}

[Serializable]
public class ChooseMiniGameLevelUIData
{
    public string uiTitleText;
    public string startText;
}
