using UnityEngine;
using TMPro;
using Zenject;

public class AllCoinsInfoPanel : MonoBehaviour
{
    [Header("Internal References")]
    [Space]
    [SerializeField] private TextMeshProUGUI coinsText;

    private ResourcesManager _resourcesManager;

    private void Start()
    {
        _resourcesManager.OnGeneralCoinsAmountChanged += GeneralCoinsAmountChanged_ExecuteReaction;

        coinsText.text = $"{_resourcesManager.WholeCoinsAmount}";
    }

    private void OnDestroy()
    {
        _resourcesManager.OnGeneralCoinsAmountChanged -= GeneralCoinsAmountChanged_ExecuteReaction;
    }

    #region Zenject
    [Inject]
    private void Construct(ResourcesManager resourcesManager)
    {
        _resourcesManager = resourcesManager;
    }
    #endregion Zenject

    private void GeneralCoinsAmountChanged_ExecuteReaction(int coins)
    {
        coinsText.text = $"{coins}";
    }
}
