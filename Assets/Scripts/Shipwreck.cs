using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shipwreck : MonoBehaviour
{
    private Inventory inventory;

    // Start is called before the first frame update
    void Awake()
    {
        inventory = GetComponent<Inventory>();
    }

    public void Initialize(Inventory shipInventory)
    {
        inventory.AddMissingItems();
        foreach (Item.Type type in Item.GetAllEnums())
        {
            int count = (int)shipInventory.GetCountOfType(type);
            inventory.AddItem(type, count);
        }
        inventory.AddCoins(shipInventory.GetCoins());
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Inventory playerInventory = collision.GetComponent<Inventory>();
            foreach (Item.Type type in Item.GetAllEnums())
            {
                int count = (int)inventory.GetCountOfType(type);
                playerInventory.AddItem(type, count);
            }
            playerInventory.AddCoins(inventory.GetCoins());
            Destroy(gameObject);
        }
    }


}
