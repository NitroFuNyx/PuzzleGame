using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ChooseGameLevelPanel : MonoBehaviour
{
    [Header("Game Level Data")]
    [Space]
    [SerializeField] private GameLevelStates levelState = GameLevelStates.Available_New;
    [SerializeField] private GameLevelTypes gameType = GameLevelTypes.Puzzle;
    [SerializeField] private int gameLevelIndex = 0;
    [Header("Button Images")]
    [Space]
    [SerializeField] private Image buttonImage;
    [SerializeField] private Image darkFilterImage;
    [SerializeField] private Image lockImage;
    [Header("Level Texts")]
    [Space]
    [SerializeField] private TextMeshProUGUI bestTime_Start_Text;
    [SerializeField] private TextMeshProUGUI bestTime_Value_Text;
    [Header("Cost Panel")]
    [Space]
    [SerializeField] private PanelActivationManager costPanel;
    [SerializeField] private TextMeshProUGUI costAmountText;
    [Header("Durations")]
    [Space]
    [SerializeField] private float changeAlphaDuration = 0.01f;

    private void Start()
    {
        SetPanelUIData();
    }

    public void SetPanelUIData()
    {
        if(levelState == GameLevelStates.Locked)
        {
            SetLockedStateUI();
        }
        else
        {
            SetAvailableStateUI();
        }
    }

    private void SetLockedStateUI()
    {
        darkFilterImage.DOFade(1f, changeAlphaDuration);
        lockImage.DOFade(1f, changeAlphaDuration);

        bestTime_Start_Text.text = "";
        bestTime_Value_Text.text = "";

        costPanel.ShowPanel();

        // set cost text
    }

    private void SetAvailableStateUI()
    {
        darkFilterImage.DOFade(0f, changeAlphaDuration);
        lockImage.DOFade(0f, changeAlphaDuration);

        if (levelState == GameLevelStates.Available_New)
        {
            bestTime_Start_Text.text = "start";
            bestTime_Value_Text.text = "";
        }
        else if(levelState == GameLevelStates.Available_Started)
        {
            bestTime_Start_Text.text = "best time";
            bestTime_Value_Text.text = "01:01"; // set time
        }

        costPanel.HidePanel();
    }
}
