using UnityEngine;
using Zenject;

public class ControllersInstaller : MonoInstaller
{
    [Header("References")]
    [Space]
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private LanguageManager languageManager;
    [SerializeField] private PlayerDataManager playerDataManager;

    public override void InstallBindings()
    {
        Container.Bind<AudioManager>().FromInstance(audioManager).AsSingle().NonLazy();
        Container.Bind<InputManager>().FromInstance(inputManager).AsSingle().NonLazy();
        Container.Bind<LanguageManager>().FromInstance(languageManager).AsSingle().NonLazy();
        Container.Bind<PlayerDataManager>().FromInstance(playerDataManager).AsSingle().NonLazy();
    }
}
