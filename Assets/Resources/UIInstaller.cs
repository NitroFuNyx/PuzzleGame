using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [Header("References")]
    [Space]
    [SerializeField] private MainUI mainUI;
    [SerializeField] private MainScreenUI mainScreenUI;
    [SerializeField] private SettingsUI settingsUI;
    [SerializeField] private MiniGameUI miniGameUI;

    public override void InstallBindings()
    {
        Container.Bind<MainUI>().FromInstance(mainUI).AsSingle().NonLazy();
        Container.Bind<MainScreenUI>().FromInstance(mainScreenUI).AsSingle().NonLazy();
        Container.Bind<SettingsUI>().FromInstance(settingsUI).AsSingle().NonLazy();
        Container.Bind<MiniGameUI>().FromInstance(miniGameUI).AsSingle().NonLazy();
    }
}
