using UnityEngine;
using TMPro;

public class MiniGameFinishedPanel : MonoBehaviour
{
    [Header("Texts")]
    [Space]
    [SerializeField] private TextMeshProUGUI coinsText;

    public void SetCoinsText(int coins)
    {
        coinsText.text = $"{coins}";
    }
}
