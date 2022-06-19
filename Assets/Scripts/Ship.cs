using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Faction
{
    NONE,
    PLAYER,
    MERCHANT,
    PIRATE,
    FROG
}

public class Ship : MonoBehaviour
{
    private Inventory inventory;

    [SerializeField]
    private string shipName;
    [SerializeField]
    private Faction faction;


    private HealthComponent health;

    private void Start()
    {
        //temp
        inventory.AddMissingItems();
        inventory.RandomizePort();
    }

    private void Awake()
    {
        health = GetComponent<HealthComponent>();
        inventory = GetComponent<Inventory>();
    }

    public Inventory GetInventory()
    {
        return inventory;
    }

    public string GetName()
    {
        return shipName;
    }
}
