using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    private Inventory inventory;

    [SerializeField]
    private string shipName;

    private void Start()
    {
        //temp
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
}
