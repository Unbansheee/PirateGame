using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

enum Faction
{
    NONE,
    PLAYER,
    MERCHANT,
    PIRATE,
    FROG
}

public class Ship : MonoBehaviour, IDamageable 
{
    private Inventory inventory;

    [SerializeField]
    private string shipName;
    [SerializeField]
    private Faction faction;

    private void Start()
    {
        //temp
        inventory.AddMissingItems();
        inventory.RandomizePort();
    }

    private void Awake()
    {
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

    
    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void DamageReceived(float damage)
    {

    }
}

