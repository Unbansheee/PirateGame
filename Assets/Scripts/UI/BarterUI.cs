using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class BarterUI : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private TextMeshProUGUI PortCoins;
    [SerializeField]
    private TextMeshProUGUI ShipCoins;
    [SerializeField]
    private List<ItemEntryUI> itemBoxes;
    [SerializeField]
    private TextMeshProUGUI CoinsCount;
    [SerializeField]
    private TextMeshProUGUI ArrowLeft;
    [SerializeField]
    private TextMeshProUGUI ArrowRight;
    [SerializeField]
    private Button ConfirmButton;
    [SerializeField]
    private Button CancelButton;

    private Barter barter;

    private void Awake()
    {
        //canvas.enabled = false;
    }

    public void Initialize(TradingPort port, Ship ship)
    {
        //canvas.enabled = true;
        gameObject.SetActive(true);
        barter = new Barter(port.GetInventory(), ship.GetInventory());
        PortCoins.SetText("" + port.GetInventory().GetCoins());
        ShipCoins.SetText("" + ship.GetInventory().GetCoins());
        CoinsCount.SetText("0");
        ArrowLeft.enabled = false;
        ArrowRight.enabled = false;
        itemBoxes.ForEach((item) => item.Initialize(this, port.GetInventory(), ship.GetInventory()));
    }

    public void OnMenuUpdate()
    {
        int sum = barter.CalculatePayment();
        ArrowLeft.enabled = sum < 0;
        ArrowRight.enabled = sum > 0;
        CoinsCount.SetText("" + sum);
        if (barter.IsValidTrade())
        {
            CoinsCount.color = Color.green;
            ConfirmButton.interactable = true;
        }
        else
        {
            CoinsCount.color = Color.red;
            ConfirmButton.interactable = false;
        }
    }

    public void ExitTrade()
    {
        gameObject.SetActive(false);
        GameManager.GUI.gameObject.SetActive(true);
        GameManager.Player.IsTrading = false;
    }

    public void ConfirmTrade()
    {
        barter.ConfirmTrade();
        ExitTrade();
    }

    public Barter GetTrade()
    {
        return barter;
    }

}
