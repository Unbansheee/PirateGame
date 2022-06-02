using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemEntryUI : MonoBehaviour
{
    [SerializeField]
    private Item.Type itemType;
    [SerializeField]
    private TextMeshProUGUI ItemName;
    [SerializeField]
    private TextMeshProUGUI ItemPrice;
    [SerializeField]
    private TextMeshProUGUI ArrowLeft;
    [SerializeField]
    private TextMeshProUGUI ArrowRight;

    [SerializeField]
    private Slider Slider;
    [SerializeField]
    private Button AddToPortButton;
    [SerializeField]
    private Button AddToShipButton;

    [SerializeField]
    private TextMeshProUGUI PortMax;
    [SerializeField]
    private TextMeshProUGUI ShipMax;
    [SerializeField]
    private TextMeshProUGUI ItemCount;

    private BarterUI barterMenu;
    private int tradeDifference;
    private int portMax;
    private int shipMax;
    private int price;

    public void Initialize(BarterUI barterMenu, Inventory port, Inventory ship)
    {
        this.barterMenu = barterMenu;
        tradeDifference = 0;
        portMax = (int)port.GetCountOfType(itemType);
        shipMax = (int)ship.GetCountOfType(itemType);
        price =   (int)port.GetPriceOfType(itemType);

        ItemName.SetText(Item.GetName(itemType));
        ItemPrice.SetText("" + price);
        Slider.maxValue = portMax;
        Slider.minValue = -shipMax;
        PortMax.SetText("" + portMax);
        ShipMax.SetText("" + shipMax);
        Slider.value = 0;
        OnSliderChange();
    }

    public void OnSliderChange()
    {
        tradeDifference = (int)Slider.value;
        AddToPortButton.interactable = shipMax > -tradeDifference;
        AddToShipButton.interactable = portMax < tradeDifference;
        ItemCount.SetText("" + tradeDifference + " (" + (tradeDifference * price) + ")");
        ArrowLeft.enabled = tradeDifference < 0;
        ArrowRight.enabled = tradeDifference > 0;
        barterMenu.GetTrade().SetTotalSoldToPort(itemType, -tradeDifference);
        barterMenu.OnMenuUpdate();
    }

    public int GetCountDifference()
    {
        return tradeDifference;
    }

    public int GetPriceDifference()
    {
        return tradeDifference * price;
    }

}
