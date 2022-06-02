using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barter// : MonoBehaviour
{
    private Inventory port;
    private Inventory ship;
    private Dictionary<Item.Type, int> transaction;
    private int payment;

    public Barter(Inventory port, Inventory ship)
    {
        this.port = port;
        this.ship = ship;
        transaction = new Dictionary<Item.Type, int>();
        foreach (Item.Type iType in Item.GetAllEnums())
        {
            transaction[iType] = 0;
        }
    }

    // Calculates the net flow of coins from the current trade
    // Negative payyment means that the port pays the ship
    public int CalculatePayment()
    {
        int sum = 0;
        foreach (Item.Type itemType in transaction.Keys)
        {
            int? price = port.GetPriceOfType(itemType);
            if (price != null)
            {
                sum += transaction[itemType] * (int)port.GetPriceOfType(itemType).Value;
            }
        }
        payment = sum;
        return payment;
    }

    // If negative trade flow of resources goes to
    public void SetTotalSoldToPort(Item.Type type, int count)
    {
        // TODO fix to check for bounds
        //int dif = transaction[type] - count;
        transaction[type] = -count;
        CalculatePayment();
        //SellToPort(type, dif);
    }
    

    // Updates the transaction to sell up to 'count' items to the port
    public void SellToPort(Item.Type type, int count)
    {
        int? portCount = port.GetCountOfType(type);
        int? shipCount = ship.GetCountOfType(type);
        if (portCount != null && shipCount != null)
        {
            int portSpace = Inventory.CountMax() - (int)portCount - transaction[type];
            int trade = Mathf.Min(portSpace, count, (int)shipCount);
            if (trade != 0)
            {
                transaction[type] += trade;
                CalculatePayment();
            }
        }
    }

    // Updates the transaction to buy up to 'count' items to the port
    public void BuyFromPort(Item.Type type, int count)
    {
        int? portCount = port.GetCountOfType(type);
        int? shipCount = ship.GetCountOfType(type);
        if (portCount != null && shipCount != null)
        {
            int shipSpace = Inventory.CountMax() - (int)shipCount - transaction[type];
            int trade = Mathf.Min(shipSpace, count, (int)portCount);
            if (trade != 0)
            {
                transaction[type] -= trade;
                CalculatePayment();
            }
        }
    }

    // Returns true if both parties have the funds to pay for the transaction
    public bool IsValidTrade()
    {
        return payment <= 0 ? port.GetCoins() >= -payment : ship.GetCoins() >= payment;
    }

    // Exits the trade
    public void CancelTrade()
    {
        // :bigbird:
    }

    // If valid, transfers items and coins between traders.
    public void ConfirmTrade()
    {
        if (!IsValidTrade())
        {
            return;
        }
        foreach (Item.Type itemType in transaction.Keys)
        {
            int count = transaction[itemType];
            if (count < 0)
            {
                port.RemoveItem(itemType, count);
                ship.AddItem(itemType, count);
            }
            else if (count > 0)
            {
                port.AddItem(itemType, count);
                ship.RemoveItem(itemType, count);
            }
        }
        if (payment < 0)
        {
            port.RemoveCoins(payment);
            ship.AddCoins(payment);
        }
        else if (payment > 0)
        {
            port.AddCoins(payment);
            ship.RemoveCoins(payment);
        }
    }

}
