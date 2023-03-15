using System.Collections.Generic;
using UnityEngine;

public class PoolItemsManager : MonoBehaviour
{
    [Header("Pool Data")]
    [Space]
    [SerializeField] private int poolSize = 50;
    [Header("Active Pools")]
    [Space]
    [SerializeField] private List<PoolItem> miniGameItemsPool = new List<PoolItem>();

    private void Start()
    {
        CreateEnemiesPool();
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

    private void CreateEnemiesPool()
    {
        PoolItem poolItemPrefab = null;

        for (int i = 0; i < poolSize; i++)
        {
            PoolItem poolItem = Instantiate(poolItemPrefab, Vector3.zero, Quaternion.identity, transform);
            poolItem.gameObject.SetActive(false);
            //enemiesPool.Add(poolItem);
            poolItem.name = $"Enemy {i}";
        }
    }
}