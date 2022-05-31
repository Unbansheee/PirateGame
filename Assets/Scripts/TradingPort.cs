using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using UnityEngine.Serialization;


public class TradingPort : MonoBehaviour
{
    //private BoxCollider2D bc;
    private Inventory inventory;
    private TMPro.TextMeshPro text;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            portEntered = true;
            Debug.Log("in");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            portEntered = true;
            Debug.Log("out");
        }
    }
    
    void UpdatePrices()
    {
        
    }


}
