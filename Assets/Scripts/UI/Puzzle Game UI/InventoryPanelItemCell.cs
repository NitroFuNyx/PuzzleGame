using UnityEngine;
using UnityEngine.UI;

public class InventoryPanelItemCell : MonoBehaviour
{
    [Header("Images")]
    [Space]
    [SerializeField] private Image itemImage;

    public void SetItemImage(Sprite sprite)
    {
        itemImage.sprite = sprite;
    }
}
