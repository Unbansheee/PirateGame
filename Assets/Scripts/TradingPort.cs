using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TradingPort : MonoBehaviour
{
    [SerializeField]
    private Canvas tradeWindow;

    [SerializeField]
    private TMPro.TextMeshPro textObject;

    [SerializeField]
    private string portName;

    private Inventory inventory;
    private TMPro.TextMeshPro text;

    public int HealingCost { get; set; } = 75;

    void Awake()
    {
        inventory = gameObject.GetComponent<Inventory>() as Inventory;
    }

    // Start is called before the first frame update
    void Start()
    {
        textObject.text = portName;

        //temp
        inventory.RandomizePort();
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            string message = "Press 'T' to Trade";
            if (GameManager.Player.GetInventory().GetCoins() >= HealingCost 
                    && !GameManager.Player.GetComponent<HealthComponent>().IsFullHealth())
            {
                message += "\nPress 'H' to pay " + HealingCost + " and repair all damage";
            }
            GameManager.GUI.ToggleMessage(true, message);
            collision.GetComponent<Ship>().OnEnterPort(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.GUI.ToggleMessage(false);
            EndTrade();
            collision.GetComponent<Ship>().OnLeavePort();
        }
    }
    
    private void UpdatePrices()
    {
        
    }

    public Inventory GetInventory()
    {
        return inventory;
    }

    public string GetName()
    {
        return portName;
    }

    // Returns true if 
    public bool StartTrade(Ship ship)
    {
        if (ship)
        {
            GameManager.GUI.gameObject.SetActive(false);
            GameManager.BarterMenu.Initialize(this, ship);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void EndTrade()
    {
        GameManager.BarterMenu.gameObject.SetActive(false);
        GameManager.GUI.gameObject.SetActive(true);
    }

}
