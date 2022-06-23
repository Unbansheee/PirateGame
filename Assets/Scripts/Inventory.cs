using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


[System.Serializable]
public class ItemEntry
{
    [HideInInspector]
    public const int countMax = 100;
    [HideInInspector]
    public const int priceMax = 100;

    [SerializeField]
    public Item.Type type;

    [SerializeField]
    [Range(0, countMax)]
    public int count;

    [SerializeField]
    [Range(1, priceMax)]
    public int price = 1;
}

public class Inventory : MonoBehaviour
{
    [HideInInspector]
    const int coinsMax = 1000;
    
    [SerializeField]
    [Range(-1, coinsMax)]
    private int coins = 0;

    public bool shouldRandomize = true;
    
    [SerializeField]
    public List<ItemEntry> items;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Adds "count" itemss to the inventory and returns the number that do not fit
    public int AddItem(Item.Type itemType, int count)
    {
        ItemEntry entry = GetEntryOfType(itemType);
        int dif = count + entry.count - ItemEntry.countMax;
        entry.count = Math.Min(entry.count + count, ItemEntry.countMax);
        return Mathf.Max(dif, 0);
    }

    // Removes and returns up to "count" items
    public int RemoveItem(Item.Type itemType, int count)
    {
        ItemEntry entry = GetEntryOfType(itemType);
        int dif = Math.Min(entry.count, count);
        entry.count -= dif;
        return dif;
    }

    public void SetCoins(int coins)
    {
        this.coins = Mathf.Min(coins, coinsMax);
    }

    public int GetCoins()
    {
        return this.coins;
    }

    public void AddCoins(int count)
    {
        SetCoins(coins + count);
    }

    public void RemoveCoins(int count)
    {
        coins = Mathf.Max(0, coins - count);
    }

    // Returns the entry of passed type, or null if no match is found
    private ItemEntry GetEntryOfType(Item.Type type)
    {
        for (int index = 0; index < items.Count; ++index)
        {
            if (items[index].type == type)
            {
                return items[index];
            }
        }
        return null;
    }

    public int? GetPriceOfType(Item.Type type)
    {
        return GetEntryOfType(type)?.price;
    }

    public int? GetCountOfType(Item.Type type)
    {
        return GetEntryOfType(type)?.count;
    }

    // Adds missing ItemEntries with a count of zero and default prices
    public void AddMissingItems()
    {
        HashSet<Item.Type> existing = items.Select((entry) => entry.type).ToHashSet<Item.Type>();
        foreach (Item.Type type in Item.GetAllEnums())
        {
            if (!existing.Contains(type))
            {
                ItemEntry entry = new()
                {
                    type = type,
                    count = 0,
                    price = Item.GetBasePrice(type),
                };
                items.Add(entry);
            }
        }
    }

    public static int CountMax()
    {
        return ItemEntry.countMax;
    }

    // Randomizes the item prices and counts in theh inventory
    public void RandomizePort()
    {
        if (shouldRandomize)
        {
            foreach (ItemEntry entry in items)
            {
                float priceRand = UnityEngine.Random.Range(0.75f, 1.25f);
                int basePrice = Item.GetBasePrice(entry.type);
                entry.price = Math.Min(ItemEntry.priceMax, (int)(basePrice * priceRand));

                float countRand = UnityEngine.Random.Range(0.5f, 1.5f);
                entry.count = (int)((ItemEntry.countMax - basePrice)/2 * countRand);
                entry.count = Math.Min(ItemEntry.countMax, entry.count);
            } 
        }
        
    }

}
