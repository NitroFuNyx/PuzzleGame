using UnityEngine;
using UnityEngine.UI;

public class LevelRestarter : MonoBehaviour
{
    [SerializeField] private CurrentGameManager currentGameManager;

    private Button playAgainButton;

    private void Start()
    {
        playAgainButton = GetComponent<Button>();
        playAgainButton.onClick.AddListener(OnButtonPressed);
    }


    private void OnDestroy()
    {
        playAgainButton.onClick.RemoveListener(OnButtonPressed);
    }

    private void OnButtonPressed()
    {
        currentGameManager.ActivateGameLevelEnvironment(0, GameLevelTypes.Puzzle);
    }
}