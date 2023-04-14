using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ChangeLanguageButtonSpriteSwitcher : MonoBehaviour
{
    private LanguageManager _languageManager;
    [Header("Target image")]
    [SerializeField] private Image buttonImage;

    [Header("Language sprites")]
    [SerializeField] private Sprite englishLanguage;
    [SerializeField] private Sprite ukrainianLanguage;
    [SerializeField] private Sprite spanishLanguage;
    [SerializeField] private Sprite otherLanguage;


    [Inject]
    private void InjectDependencies(LanguageManager languageManager)
    {
        _languageManager = languageManager;
    }

    // Start is called before the first frame update
    void Start()
    {
        _languageManager.OnSpriteChanged += SwitchSprite;
    }

    private void OnDestroy()
    {
        _languageManager.OnSpriteChanged -= SwitchSprite;

    }

    private void SwitchSprite(Languages language)
    {
        switch (language)
        {
            case Languages.English:
                buttonImage.sprite = englishLanguage;
                break;
            case Languages.Ukrainian:
                buttonImage.sprite = ukrainianLanguage;
                break;
            case Languages.Spanish:
                buttonImage.sprite =spanishLanguage;
                break;
            case Languages.Other:
                buttonImage.sprite = otherLanguage;
                break;
            default:
                throw new WarningException("Unexpected language received");
        }
    }

   
}
