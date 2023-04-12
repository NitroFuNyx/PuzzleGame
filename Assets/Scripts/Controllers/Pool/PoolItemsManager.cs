using System.Collections.Generic;
using UnityEngine;

public class PoolItemsManager : MonoBehaviour
{
    [Header("Pool Data")]
    [Space]
    [SerializeField] private int miniGameCoinItemsPoolSize = 50;
    [Header("Holders")]
    [Space]
    [SerializeField] private Transform spawnedItemsHolder;
    [SerializeField] private Transform vfxHolder;
    [Header("Active Kitchen Mini Game Pools")]
    [Space]
    [SerializeField] private List<PoolItem> miniGameItemsCoinsPool_Coin1 = new List<PoolItem>();
    [SerializeField] private List<PoolItem> miniGameItemsCoinsPool_Coin2 = new List<PoolItem>();
    [SerializeField] private List<PoolItem> miniGameItemsCoinsPool_Coin5 = new List<PoolItem>();
    [SerializeField] private List<PoolItem> miniGameItemsCoinsPool_Coin10 = new List<PoolItem>();
    [Space]
    [SerializeField] private List<PoolItem> miniGameItemsCoinsPool_Debuff = new List<PoolItem>();
    [Space]
    [SerializeField] private List<PoolItem> miniGameItemsBonusesPool_AdditionalTime = new List<PoolItem>();
    [SerializeField] private List<PoolItem> miniGameItemsBonusesPool_DoubleCoins = new List<PoolItem>();
    [SerializeField] private List<PoolItem> miniGameItemsBonusesPool_CoinsMagnet = new List<PoolItem>();
    [SerializeField] private List<PoolItem> miniGameItemsBonusesPool_Shield = new List<PoolItem>();
    [Header("Mini Game Prefabs")]
    [Space]
    [SerializeField] private PoolItem miniGameItemCoinPrefab_1Coin;
    [SerializeField] private PoolItem miniGameItemCoinPrefab_2Coins;
    [SerializeField] private PoolItem miniGameItemCoinPrefab_5Cois;
    [SerializeField] private PoolItem miniGameItemCoinPrefab_10Coins;
    [Space]
    [SerializeField] private PoolItem miniGameItemCoinPrefab_Debuff;
    [Space]
    [SerializeField] private PoolItem miniGameItemBonusPrefab_AdditionalTime;
    [SerializeField] private PoolItem miniGameItemBonusPrefab_DoubleCoins;
    [SerializeField] private PoolItem miniGameItemBonusPrefab_CoinsMagnet;
    [SerializeField] private PoolItem miniGameItemBonusPrefab_Shield;

    private Dictionary<KitchenMiniGameItems, List<PoolItem>> kitchenMiniGameItemsDictionary = new Dictionary<KitchenMiniGameItems, List<PoolItem>>();
    private Dictionary<List<PoolItem>, Transform> kitchenMiniGameItemsListsHoldersDictionary = new Dictionary<List<PoolItem>, Transform>();

    public Transform SpawnedItemsHolder { get => spawnedItemsHolder; }

    private void Awake()
    {
        FillKitchenMiniGameItemsDictionary();
    }

    private void Start()
    {
        CreatePool(miniGameItemCoinPrefab_1Coin, miniGameItemsCoinsPool_Coin1, "Mini Game Item Coin 1", miniGameCoinItemsPoolSize);
        CreatePool(miniGameItemCoinPrefab_2Coins, miniGameItemsCoinsPool_Coin2, "Mini Game Item Coin 2", miniGameCoinItemsPoolSize);
        CreatePool(miniGameItemCoinPrefab_5Cois, miniGameItemsCoinsPool_Coin5, "Mini Game Item Coin 5", miniGameCoinItemsPoolSize);
        CreatePool(miniGameItemCoinPrefab_10Coins, miniGameItemsCoinsPool_Coin10, "Mini Game Item Coin 10", miniGameCoinItemsPoolSize);

        CreatePool(miniGameItemCoinPrefab_Debuff, miniGameItemsCoinsPool_Debuff, "Mini Game Item Debuff", miniGameCoinItemsPoolSize);

        CreatePool(miniGameItemBonusPrefab_AdditionalTime, miniGameItemsBonusesPool_AdditionalTime, "Mini Game Item Bonus Additional Time", miniGameCoinItemsPoolSize);
        CreatePool(miniGameItemBonusPrefab_DoubleCoins, miniGameItemsBonusesPool_DoubleCoins, "Mini Game Item Bonus Double Coins", miniGameCoinItemsPoolSize);
        CreatePool(miniGameItemBonusPrefab_CoinsMagnet, miniGameItemsBonusesPool_CoinsMagnet, "Mini Game Item Bonus Coins Magnet", miniGameCoinItemsPoolSize);
        CreatePool(miniGameItemBonusPrefab_Shield, miniGameItemsBonusesPool_Shield, "Mini Game Item Bonus Shield", miniGameCoinItemsPoolSize);
    }

    public PoolItem SpawnItemFromPool(Vector3 _spawnPos, Quaternion _rotation, Transform _parent, KitchenMiniGameItems itemType)
    {
        PoolItem poolItem = null;
        List<PoolItem> poolItemsList = kitchenMiniGameItemsDictionary[itemType];

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

    public void ReturnItemToPool(PoolItem _poolItem, KitchenMiniGameItems itemType)
    {
        List<PoolItem> poolItemsList = kitchenMiniGameItemsDictionary[itemType];

        _poolItem.gameObject.SetActive(false);
        _poolItem.transform.SetParent(kitchenMiniGameItemsListsHoldersDictionary[poolItemsList]);
        _poolItem.transform.localPosition = Vector3.zero;
        poolItemsList.Add(_poolItem);
    }

    public void ReturnKitchenMiniGamePoolListItems(List<PoolItem> list, KitchenMiniGameItems itemsType)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if(list[i].gameObject.activeInHierarchy)
            ReturnItemToPool(list[i], itemsType);
        }
    }

    public void ReturnItems_KitchenMiniGame()
    {
        List<PoolItem> list = new List<PoolItem>();

        for(int i = 0; i < spawnedItemsHolder.childCount; i++)
        {
            if(spawnedItemsHolder.GetChild(i).TryGetComponent(out PoolItem item))
            {
                list.Add(item);
            }
        }

        for(int i = 0; i < list.Count; i++)
        {
            list[i].ResetItemComponent();
            ReturnItemToPool(list[i], list[i].KitchenMiniGameItem.ItemType);
        }
    }

    private void CreatePool(PoolItem poolItemPrefab, List<PoolItem> poolItemsList, string itemName, int poolSize)
    {
        GameObject poolItemsParent = new GameObject();
        poolItemsParent.transform.SetParent(transform);
        poolItemsParent.name = $"{itemName} Items Parent";
        poolItemsParent.transform.position = new Vector3(100f, 0f, 0f);

        kitchenMiniGameItemsListsHoldersDictionary.Add(poolItemsList, poolItemsParent.transform);

        for (int i = 0; i < poolSize; i++)
        {
            PoolItem poolItem = Instantiate(poolItemPrefab, Vector3.zero, Quaternion.identity, poolItemsParent.transform);
            poolItem.CashComponents(vfxHolder);
            poolItem.transform.localPosition = Vector3.zero;
            poolItem.gameObject.SetActive(false);
            poolItemsList.Add(poolItem);
            poolItem.name = $"{itemName} {i}";
        }
    }

    private void FillKitchenMiniGameItemsDictionary()
    {
        kitchenMiniGameItemsDictionary.Add(KitchenMiniGameItems.Coin_1, miniGameItemsCoinsPool_Coin1);
        kitchenMiniGameItemsDictionary.Add(KitchenMiniGameItems.Coins_2, miniGameItemsCoinsPool_Coin2);
        kitchenMiniGameItemsDictionary.Add(KitchenMiniGameItems.Coins_5, miniGameItemsCoinsPool_Coin5);
        kitchenMiniGameItemsDictionary.Add(KitchenMiniGameItems.Coins_10, miniGameItemsCoinsPool_Coin10);
        kitchenMiniGameItemsDictionary.Add(KitchenMiniGameItems.Debuff, miniGameItemsCoinsPool_Debuff);
        kitchenMiniGameItemsDictionary.Add(KitchenMiniGameItems.Bonus_AdditionalTime, miniGameItemsBonusesPool_AdditionalTime);
        kitchenMiniGameItemsDictionary.Add(KitchenMiniGameItems.Bonus_DoubleCoins, miniGameItemsBonusesPool_DoubleCoins);
        kitchenMiniGameItemsDictionary.Add(KitchenMiniGameItems.Bonus_CoinsMagnet, miniGameItemsBonusesPool_CoinsMagnet);
        kitchenMiniGameItemsDictionary.Add(KitchenMiniGameItems.Bonus_Shield, miniGameItemsBonusesPool_Shield);
    }
}