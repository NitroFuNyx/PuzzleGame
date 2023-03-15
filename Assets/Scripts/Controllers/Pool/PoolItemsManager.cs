using System.Collections.Generic;
using UnityEngine;

public class PoolItemsManager : MonoBehaviour
{
    [Header("Pool Data")]
    [Space]
    [SerializeField] private int poolSize = 50;
    [Header("Active Pools")]
    [Space]
    [SerializeField] private List<PoolItem> miniGameItemsBonusesPool = new List<PoolItem>();
    [Header("Mini Game Prefabs")]
    [Space]
    [SerializeField] private PoolItem miniGameItemBonusPrefab;

    private void Start()
    {
        CreatePool(miniGameItemBonusPrefab, miniGameItemsBonusesPool, "Mini Game Item Bonus");
    }

    public PoolItem SpawnItemFromPool(Vector3 _spawnPos, Quaternion _rotation, Transform _parent, List<PoolItem> poolItemsList)
    {
        PoolItem poolItem = null;

        for (int i = 0; i < poolItemsList.Count; i++)
        {
            if (!poolItemsList[i].gameObject.activeInHierarchy)
            {
                poolItem = poolItemsList[i];
                break;
            }
        }

        if (poolItem != null)
        {
            poolItem.transform.SetParent(_parent);
            poolItem.transform.position = _spawnPos;
            poolItem.transform.rotation = _rotation;
            poolItem.gameObject.SetActive(true);
            poolItemsList.Remove(poolItem);
        }

        return poolItem;
    }

    public void ReturnItemToPool(PoolItem _poolItem, List<PoolItem> poolItemsList)
    {
        _poolItem.gameObject.SetActive(false);
        _poolItem.transform.SetParent(transform);
        _poolItem.transform.position = Vector3.zero;
        poolItemsList.Add(_poolItem);
    }

    private void CreatePool(PoolItem poolItemPrefab, List<PoolItem> poolItemsList, string itemName)
    {
        GameObject poolItemsParent = new GameObject();
        poolItemsParent.transform.SetParent(transform);
        poolItemsParent.name = $"{itemName} Items Parent";
        poolItemsParent.transform.position = new Vector3(100f, 0f, 0f);

        for (int i = 0; i < poolSize; i++)
        {
            PoolItem poolItem = Instantiate(poolItemPrefab, Vector3.zero, Quaternion.identity, poolItemsParent.transform);
            poolItem.gameObject.SetActive(false);
            poolItemsList.Add(poolItem);
            poolItem.name = $"{itemName} {i}";
        }
    }
}