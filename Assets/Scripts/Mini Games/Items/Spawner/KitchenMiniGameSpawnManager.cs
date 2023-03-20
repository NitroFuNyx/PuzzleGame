using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenMiniGameSpawnManager : MonoBehaviour
{
    [Header("Spawners")]
    [Space]
    [SerializeField] private List<KitchenMiniGameItemsSpawner> spawnersList = new List<KitchenMiniGameItemsSpawner>();
    [Header("Spawn Data")]
    [Space]
    [SerializeField] private float spawnDelay = 0.5f;

    private bool canSpawn = false;

    private Dictionary<int, KitchenMiniGameItems> itemsSpawnPossibilityDictionary = new Dictionary<int, KitchenMiniGameItems>();

    private void Awake()
    {
        FillItemsSpawnPossibilityDictionary();
    }

    [ContextMenu("Spawn")]
    public void StartSpawningItems()
    {
        canSpawn = true;
        StartCoroutine(StartSpawningItemsCoroutine());
    }

    public void StopSpawning()
    {
        canSpawn = false;
    }

    private void FillItemsSpawnPossibilityDictionary()
    {
        itemsSpawnPossibilityDictionary.Add(0, KitchenMiniGameItems.Coin_1);
        itemsSpawnPossibilityDictionary.Add(1, KitchenMiniGameItems.Coin_1);
        itemsSpawnPossibilityDictionary.Add(2, KitchenMiniGameItems.Coin_1);
        itemsSpawnPossibilityDictionary.Add(3, KitchenMiniGameItems.Coin_1);
        itemsSpawnPossibilityDictionary.Add(4, KitchenMiniGameItems.Coin_1);
        itemsSpawnPossibilityDictionary.Add(5, KitchenMiniGameItems.Coin_1);
        itemsSpawnPossibilityDictionary.Add(6, KitchenMiniGameItems.Coins_2);
        itemsSpawnPossibilityDictionary.Add(7, KitchenMiniGameItems.Coins_2);
        itemsSpawnPossibilityDictionary.Add(8, KitchenMiniGameItems.Coins_2);
        itemsSpawnPossibilityDictionary.Add(9, KitchenMiniGameItems.Coins_5);
        itemsSpawnPossibilityDictionary.Add(10, KitchenMiniGameItems.Coins_5);
        itemsSpawnPossibilityDictionary.Add(11, KitchenMiniGameItems.Coins_10);
        itemsSpawnPossibilityDictionary.Add(12, KitchenMiniGameItems.Debuff);
        itemsSpawnPossibilityDictionary.Add(13, KitchenMiniGameItems.Debuff);
        itemsSpawnPossibilityDictionary.Add(14, KitchenMiniGameItems.Debuff);
        itemsSpawnPossibilityDictionary.Add(15, KitchenMiniGameItems.Debuff);
        itemsSpawnPossibilityDictionary.Add(16, KitchenMiniGameItems.Bonus_AdditionalTime);
        itemsSpawnPossibilityDictionary.Add(17, KitchenMiniGameItems.Bonus_DoubleCoins);
        itemsSpawnPossibilityDictionary.Add(18, KitchenMiniGameItems.Bonus_CoinsMagnet);
        itemsSpawnPossibilityDictionary.Add(19, KitchenMiniGameItems.Bonus_Shield);
    }

    private IEnumerator StartSpawningItemsCoroutine()
    {
        while(canSpawn)
        {
            yield return new WaitForSeconds(spawnDelay);
            int itemIndex = Random.Range(0, 20);
            int spawnerIndex = Random.Range(0, spawnersList.Count);
            spawnersList[spawnerIndex].SpawnItem(itemsSpawnPossibilityDictionary[itemIndex]);
        }
    }
}
