using UnityEngine;
using Zenject;

public class KitchenMiniGameItemsSpawner : MonoBehaviour
{
    private PoolItemsManager _poolItemsManager;

    #region Zenject
    [Inject]
    private void Construct(PoolItemsManager poolItemsManager)
    {
        _poolItemsManager = poolItemsManager;
    }
    #endregion Zenject

    public void SpawnItem(KitchenMiniGameItems item)
    {
        _poolItemsManager.SpawnItemFromPool(transform.position, Quaternion.identity, transform, item);
    }
}
