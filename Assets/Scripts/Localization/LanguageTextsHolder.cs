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
    public string uiTitleText;
    public string soundsButtonText;
    public string languageButtonText;
    public string infoButtonText;
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
