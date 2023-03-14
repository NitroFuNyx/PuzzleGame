using UnityEngine;
using Zenject;

public class GamesEnvironmentsInstaller : MonoInstaller
{
    [Header("References")]
    [Space]
    [SerializeField] private MiniGamesEnvironmentsHolder miniGamesEnvironmentsHolder;

    public override void InstallBindings()
    {
        Container.Bind<MiniGamesEnvironmentsHolder>().FromInstance(miniGamesEnvironmentsHolder).AsSingle().NonLazy();
    }
}
