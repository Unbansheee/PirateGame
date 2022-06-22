using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TradingPort : MonoBehaviour
{
    private Inventory inventory;
    private TMPro.TextMeshPro text;

    [SerializeField]
    private Canvas tradeWindow;

    [SerializeField]
    private TMPro.TextMeshPro textObject;

    [SerializeField]
    private string portName;

    private bool portEntered = false;

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

            GameManager.GUI.ToggleMessage(true, "Press 'T' to Trade");
            portEntered = true;
            //GameManager.BarterMenu.Initialize(this, collision.GetComponent<Ship>());
            collision.GetComponent<Ship>().OnEnterPort(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.GUI.ToggleMessage(false);
            portEntered = true;
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
