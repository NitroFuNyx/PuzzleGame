using UnityEngine;
using UnityEngine.UI;

public class PrivacyPolicyPanel : MonoBehaviour
{
    [Header("Internal References")]
    [Space]
    [SerializeField] private ScrollRect scrollRect;

    public void SetStartSettings()
    {
        //scrollRect.verticalNormalizedPosition -= 0f;
    }
}
