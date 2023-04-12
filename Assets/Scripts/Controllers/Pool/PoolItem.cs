using UnityEngine;

public class PoolItem : MonoBehaviour
{
    private KitchenMiniGameItem kitchenMiniGameItem;

    public KitchenMiniGameItem KitchenMiniGameItem { get => kitchenMiniGameItem; private set => kitchenMiniGameItem = value; }

    public void CashComponents(Transform vfxHolder)
    {
        if(TryGetComponent(out KitchenMiniGameItem item))
        {
            kitchenMiniGameItem = item;
            kitchenMiniGameItem.VfxHolder = vfxHolder;
        }    
    }

    public void ResetItemComponent()
    {
        kitchenMiniGameItem.ResetVFX();
    }
}