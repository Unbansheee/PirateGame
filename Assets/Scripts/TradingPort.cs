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
        Debug.Log(textObject.text);
        textObject.text = portName;

        //temp
        inventory.RandomizePort();
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            portEntered = true;
            GameManager.BarterMenu.Initialize(this, collision.GetComponent<Ship>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            portEntered = true;
            GameManager.BarterMenu.gameObject.SetActive(false);
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

}
