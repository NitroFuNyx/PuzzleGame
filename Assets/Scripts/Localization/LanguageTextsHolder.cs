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
