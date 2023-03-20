using UnityEngine;
using TMPro;
using Zenject;

public class KitchenMiniGameBonusTimer : MonoBehaviour
{
    [Header("Bonus Panel Type")]
    [Space]
    [SerializeField] private KitchenMiniGameItems bonusType;
    [Header("Texts")]
    [Space]
    [SerializeField] private TextMeshProUGUI timerText;

    private PanelActivationManager activationMnager;
    private TimersManager _timersManager;

    public PanelActivationManager ActivationMnager { get => activationMnager; private set => activationMnager = value; }

    private void Awake()
    {
        activationMnager = GetComponent<PanelActivationManager>();
    }

    private void Start()
    {
        //activationMnager.HidePanel();
    }

    #region Zenject
    [Inject]
    private void Construct(TimersManager timersManager)
    {
        _timersManager = timersManager;
    }
    #endregion Zenject

    public void UpdateTimer(float currentTime)
    {
        timerText.text = $"{_timersManager.GetHoursAndMinutesAmount((int)currentTime)}:{_timersManager.GetSecondsAmount((int)currentTime)}";
    }
}
