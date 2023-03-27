using UnityEngine;
using Zenject;

public class GamesEnvironmentsInstaller : MonoInstaller
{
    [Header("References")]
    [Space]
    [SerializeField] private MiniGamesEnvironmentsHolder miniGamesEnvironmentsHolder;
    [SerializeField] private PuzzleGamesEnvironmentsHolder puzzleGamesEnvironmentsHolder;

    public override void InstallBindings()
    {
        Container.Bind<MiniGamesEnvironmentsHolder>().FromInstance(miniGamesEnvironmentsHolder).AsSingle().NonLazy();
        Container.Bind<PuzzleGamesEnvironmentsHolder>().FromInstance(puzzleGamesEnvironmentsHolder).AsSingle().NonLazy();
    }
}
