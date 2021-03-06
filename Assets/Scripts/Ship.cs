using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField]
    private GameObject shipwreck;

    private HealthComponent healthC;
    private Shipwreck wreckInstance = null;
    public TradingPort CurrentPort { get; set; } = null;

    public bool IsTrading { get; set; } = false;

    private void Awake()
    {
        inventory = GetComponent<Inventory>();
        healthC = GetComponent<HealthComponent>();
    }

    private void Start()
    {
        inventory.AddMissingItems();
        inventory.RandomizePort();
        
        if (CompareTag("Player")) 
        {
            healthC.OnDeath.AddListener(Restart);
        }
        if (CompareTag("Player") && PersistentData.Coins != -1 && PersistentData.Health != -1f && PersistentData._Inventory != null)
        {

            inventory.items = new List<ItemEntry>(PersistentData._Inventory);
            inventory.SetCoins(PersistentData.Coins);
            healthC.SetHealth(PersistentData.Health);
            
        }
        // if (faction == Faction.PLAYER && !PersistentData.FirstLoad)
        // {
        //     healthC.SetHealth(PersistentData.Health);
        //     inventory.SetCoins(PersistentData.Coins);
        //     PersistentData.FirstLoad = false;
        // }
        
        //find gameobject by name
        
        
        
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
        if (wreckInstance)
        {
            return;
        }
        wreckInstance = Instantiate(shipwreck, transform.position, transform.rotation).GetComponent<Shipwreck>();
        wreckInstance.Initialize(inventory);
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
        if (IsAtPort())
        {
            CurrentPort.EndTrade();
        }
        IsTrading = false;
    }

    public void OnEnterPort(TradingPort port)
    {
        //healthC.HealOverTime = true;
        this.CurrentPort = port;
    }

    public void OnLeavePort()
    {
        CurrentPort = null;
        //healthC.HealOverTime = false;
    }

    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}

