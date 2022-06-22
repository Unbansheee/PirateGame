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
    public TradingPort CurrentPort { get; set; } = null;

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

    
    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    public void DamageReceived(float damage)
    {

    }

    public bool IsAtPort()
    {
        return CurrentPort != null;
    }

    public void StartBartering()
    {
        if (IsAtPort())
        {
            IsTrading = CurrentPort.StartTrade(this);
        }
    }

    public void EndBartering()
    {
        CurrentPort.EndTrade();
        IsTrading = false;
    }

    public void OnEnterPort(TradingPort port)
    {
        healthC.HealOverTime = true;
        this.CurrentPort = port;
    }

    public void OnLeavePort()
    {
        CurrentPort = null;
        healthC.HealOverTime = false;
    }
}

