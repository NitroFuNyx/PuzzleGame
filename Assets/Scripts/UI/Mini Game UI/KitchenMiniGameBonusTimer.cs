using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using Zenject;
using DG.Tweening;

public class KitchenMiniGameBonusTimer : MonoBehaviour
{
    [Header("Bonus Panel Type")]
    [Space]
    [SerializeField] private KitchenMiniGameItems bonusType;
    [Header("Texts")]
    [Space]
    [SerializeField] private TextMeshProUGUI timerText;
    [Header("Images")]
    [Space]
    [SerializeField] private Image bonusImage;
    [Header("Panel Update Data")]
    [Space]
    [SerializeField] private float changeTextColorDuration = 0.5f;
    [SerializeField] private Vector3 punchVector = new Vector3(0.2f, 0.2f, 0.2f);
    [SerializeField] private int scaleFreequency = 3;

    private PanelActivationManager activationManager;
    private TimersManager _timersManager;

    private float timeColorChangeTreshold = 3f;

    private float timerValue = 0f;

    public PanelActivationManager ActivationManager { get => activationManager; private set => activationManager = value; }
    public float TimerValue { get => timerValue; private set => timerValue = value; }

    private void Awake()
    {
        activationManager = GetComponent<PanelActivationManager>();
    }

    private void Start()
    {
        activationManager.HidePanel();
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
        timerValue = currentTime;

        if(activationManager._CanvasGroup.alpha != 1f)
        {
            activationManager.ShowPanel();
        }

        timerText.text = $"{_timersManager.GetHoursAndMinutesAmount((int)currentTime)}:{_timersManager.GetSecondsAmount((int)currentTime)}";

        if(currentTime < timeColorChangeTreshold)
        {
            if(timerText.color != Color.red)
            {
                timerText.DOColor(Color.red, changeTextColorDuration);
            }
            timerText.transform.DOKill();
            bonusImage.transform.DOKill();

            timerText.transform.DOPunchScale(punchVector, changeTextColorDuration, scaleFreequency);
            bonusImage.transform.DOPunchScale(punchVector, changeTextColorDuration, scaleFreequency);

            if (currentTime == 0f)
            {
                StartCoroutine(ResetPanelCoroutine());
            }
        }
        else if (currentTime > timeColorChangeTreshold)
        {
            ResetPanel();
        }
    }

    private void ResetPanel()
    {
        if (timerText.color != Color.white)
        {
            timerText.DOColor(Color.white, changeTextColorDuration);
        }
        timerText.transform.DOKill();
        bonusImage.transform.DOKill();

        timerText.transform.DOScale(Vector3.one, changeTextColorDuration);
        bonusImage.transform.DOScale(Vector3.one, changeTextColorDuration);
    }

    private IEnumerator ResetPanelCoroutine()
    {
        yield return new WaitForSeconds(changeTextColorDuration);
        ResetPanel();
    }
}
