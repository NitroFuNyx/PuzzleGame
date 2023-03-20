using UnityEngine;

public class PoolItem : MonoBehaviour
{
    private KitchenMiniGameItem kitchenMiniGameItem;

    public KitchenMiniGameItem KitchenMiniGameItem { get => kitchenMiniGameItem; private set => kitchenMiniGameItem = value; }

    public void CashComponents()
    {
        if(TryGetComponent(out KitchenMiniGameItem item))
        {
            kitchenMiniGameItem = item;
        }    
    }

    public void ResetItemComponent()
    {
        kitchenMiniGameItem.ResetVFX();
    }
}