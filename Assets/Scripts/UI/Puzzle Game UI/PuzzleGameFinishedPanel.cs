using UnityEngine;
using TMPro;

public class PuzzleGameFinishedPanel : MonoBehaviour
{
    [Header("Internal References")]
    [Space]
    [SerializeField] private TextMeshProUGUI timeFinishedText;

    private PanelActivationManager activationManager;

    private void Awake()
    {
        activationManager = GetComponent<PanelActivationManager>();
    }

    private void Start()
    {
        activationManager.HidePanel();
    }

    public void SetFinishTimeText(string time)
    {
        timeFinishedText.text = time;
    }
}
