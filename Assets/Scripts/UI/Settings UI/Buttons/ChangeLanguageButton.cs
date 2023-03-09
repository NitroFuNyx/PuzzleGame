using UnityEngine;
using Zenject;

public class ChangeLanguageButton : ButtonInteractionHandler
{
    [Header("Button Data")]
    [Space]
    [SerializeField] private Languages language;

    private LanguageManager _languageManager;

    #region Zenject
    [Inject]
    private void Construct(LanguageManager languageManager)
    {
        _languageManager = languageManager;
    }
    #endregion Zenject

    public override void ButtonActivated()
    {
        _languageManager.ChangeLanguage(language);
        ShowAnimation_ButtonPressed();
    }
}
