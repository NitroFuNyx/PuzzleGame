using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [Header("References")]
    [Space]
    [SerializeField] private MainUI mainUI;
    [SerializeField] private MainScreenUI mainScreenUI;
    [SerializeField] private SettingsUI settingsUI;
    [Space]
    [SerializeField] private PuzzleGameUI puzzleGameUI;
    [SerializeField] private PopItGameStateManager popItGameStateManager;
    [Space]
    [SerializeField] private MiniGameUI miniGameUI;
    [SerializeField] private KitchenMiniGameBonusTimersPanel kitchenMiniGameBonusTimers;

    public override void InstallBindings()
    {
        Container.Bind<MainUI>().FromInstance(mainUI).AsSingle().NonLazy();
        Container.Bind<MainScreenUI>().FromInstance(mainScreenUI).AsSingle().NonLazy();
        Container.Bind<SettingsUI>().FromInstance(settingsUI).AsSingle().NonLazy();

        Container.Bind<PuzzleGameUI>().FromInstance(puzzleGameUI).AsSingle().NonLazy();
        Container.Bind<PopItGameStateManager>().FromInstance(popItGameStateManager).AsSingle().NonLazy();

        Container.Bind<MiniGameUI>().FromInstance(miniGameUI).AsSingle().NonLazy();
        Container.Bind<KitchenMiniGameBonusTimersPanel>().FromInstance(kitchenMiniGameBonusTimers).AsSingle().NonLazy();
    }
}
