using UnityEngine;
using Zenject;

public class ControllersInstaller : MonoInstaller
{
    [Header("References")]
    [Space]
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private LanguageManager languageManager;
    [SerializeField] private GameDataHolder gameDataHolder;
    [SerializeField] private CurrentGameManager currentGameManager;
    [SerializeField] private TimersManager timersManager;
    [SerializeField] private ResourcesManager resourcesManager;
    [SerializeField] private PoolItemsManager poolItemsManager;
    [SerializeField] private SystemTimeManager systemTimeManager;
    [SerializeField] private DataPersistanceManager dataPersistanceManager;
    [SerializeField] private AdsManager adsManager;
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private AdsInitializer adsInitializer;

    public override void InstallBindings()
    {
        Container.Bind<AudioManager>().FromInstance(audioManager).AsSingle().NonLazy();
        Container.Bind<InputManager>().FromInstance(inputManager).AsSingle().NonLazy();
        Container.Bind<LanguageManager>().FromInstance(languageManager).AsSingle().NonLazy();
        Container.Bind<GameDataHolder>().FromInstance(gameDataHolder).AsSingle().NonLazy();
        Container.Bind<CurrentGameManager>().FromInstance(currentGameManager).AsSingle().NonLazy();
        Container.Bind<TimersManager>().FromInstance(timersManager).AsSingle().NonLazy();
        Container.Bind<ResourcesManager>().FromInstance(resourcesManager).AsSingle().NonLazy();
        Container.Bind<PoolItemsManager>().FromInstance(poolItemsManager).AsSingle().NonLazy();
        Container.Bind<SystemTimeManager>().FromInstance(systemTimeManager).AsSingle().NonLazy();
        Container.Bind<DataPersistanceManager>().FromInstance(dataPersistanceManager).AsSingle().NonLazy();
        Container.Bind<AdsManager>().FromInstance(adsManager).AsSingle().NonLazy();
        Container.Bind<CameraManager>().FromInstance(cameraManager).AsSingle().NonLazy();
        Container.Bind<AdsInitializer>().FromInstance(adsInitializer).AsSingle().NonLazy();
    }
}
