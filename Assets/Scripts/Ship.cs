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

    private HealthComponent healthC;
    private TradingPort currentPort = null;

    public bool IsTrading { get; set; } = false;

    private void Start()
    {
        healthC = GetComponent<HealthComponent>();
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

    public bool IsAtPort()
    {
        return currentPort != null;
    }

    public void StartBartering()
    {
        if (IsAtPort())
        {
            IsTrading = currentPort.StartTrade(this);
        }
    }

    public void EndBartering()
    {
        currentPort.EndTrade();
        IsTrading = false;
    }

    public void OnEnterPort(TradingPort port)
    {
        healthC.HealOverTime = true;
        this.currentPort = port;
    }

    public void OnLeavePort()
    {
        currentPort = null;
        healthC.HealOverTime = false;
    }
}

