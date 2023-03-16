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

    private Dictionary<int, KitchenMiniGameItems> itemsSpawnPossibilityDictionary = new Dictionary<int, KitchenMiniGameItems>();

    private void Awake()
    {
        FillItemsSpawnPossibilityDictionary();
    }

    [ContextMenu("Spawn")]
    public void StartSpawningItems()
    {
        StartCoroutine(StartSpawningItemsCoroutine());
    }

    private void FillItemsSpawnPossibilityDictionary()
    {
        itemsSpawnPossibilityDictionary.Add(0, KitchenMiniGameItems.Coin_1);
        itemsSpawnPossibilityDictionary.Add(1, KitchenMiniGameItems.Coin_1);
        itemsSpawnPossibilityDictionary.Add(2, KitchenMiniGameItems.Coin_1);
        itemsSpawnPossibilityDictionary.Add(3, KitchenMiniGameItems.Coins_2);
        itemsSpawnPossibilityDictionary.Add(4, KitchenMiniGameItems.Coins_2);
        itemsSpawnPossibilityDictionary.Add(5, KitchenMiniGameItems.Debuff);
        itemsSpawnPossibilityDictionary.Add(6, KitchenMiniGameItems.Debuff);
        itemsSpawnPossibilityDictionary.Add(7, KitchenMiniGameItems.Coins_5);
        itemsSpawnPossibilityDictionary.Add(8, KitchenMiniGameItems.Coins_5);
        itemsSpawnPossibilityDictionary.Add(9, KitchenMiniGameItems.Coins_10);
    }

    private IEnumerator StartSpawningItemsCoroutine()
    {
        for(int i = 0; i < spawnersList.Count; i++)
        {
            yield return new WaitForSeconds(1f);
            int index = Random.Range(0, 9);
            spawnersList[i].SpawnItem(itemsSpawnPossibilityDictionary[index]);
        }
    }
}
