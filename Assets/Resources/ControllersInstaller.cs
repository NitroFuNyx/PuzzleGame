using UnityEngine;
using Zenject;

public class ControllersInstaller : MonoInstaller
{
    [Header("References")]
    [Space]
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private InputManager inputManager;

    public override void InstallBindings()
    {
        Container.Bind<AudioManager>().FromInstance(audioManager).AsSingle().NonLazy();
        Container.Bind<InputManager>().FromInstance(inputManager).AsSingle().NonLazy();
    }
}
