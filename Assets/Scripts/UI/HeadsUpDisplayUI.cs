using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HeadsUpDisplayUI : MonoBehaviour
{
    public GameObject Ship_Gear_Full;
    public GameObject Ship_Gear_Full_2;
    public GameObject Ship_Gear_Full_3;
    public GameObject Ship_Gear_Reverse_Full;
    public GameObject Health_10;
    public GameObject Health_20;
    public GameObject Health_30;
    public GameObject Health_40;
    public GameObject Health_50;
    public GameObject Health_60;
    public GameObject Health_70;
    public GameObject Health_80;
    public GameObject Health_90;
    public GameObject Health_100;
    public TMPro.TextMeshProUGUI message;
    public TMPro.TextMeshProUGUI messageCost;
    public RectTransform Compass_Stick;

    private ShipControls controller;
    private HealthComponent health;
    private Transform objective;

    public Ship ship;

    [SerializeField]
    private TextMeshProUGUI ShipCoins;


    // Start is called before the first frame update
    void Start()
    {

        controller = GameManager.Player.GetComponent<ShipControls>();
        health = GameManager.Player.GetComponent<HealthComponent>();
        objective = GameManager.Objective;
    }

    // Update is called once per frame
    void Update()
    {
        ShipCoins.SetText("" + ship.GetInventory().GetCoins());

        if (controller != null)
        {
            switch (controller.getGear())
                    {
                        case -1:
                            Ship_Gear_Reverse_Full.SetActive(true);
                            break;
                        case 0:
                            Ship_Gear_Full.SetActive(false);
                            Ship_Gear_Reverse_Full.SetActive(false);
                            break;
                        case 1:
                            Ship_Gear_Full.SetActive(true);
                            Ship_Gear_Full_2.SetActive(false);
                            break;
                        case 2:
                            Ship_Gear_Full_2.SetActive(true);
                            Ship_Gear_Full_3.SetActive(false);
                            break;
                        case 3:
                            Ship_Gear_Full_3.SetActive(true);
                            break;
                        default:
                            Debug.LogWarning("UI doesnt know what to do");
                            break;
                    }
        }
        if (health != null)
        {
            Health_100.SetActive(false);
            Health_90.SetActive(false);
            Health_80.SetActive(false);
            Health_70.SetActive(false);
            Health_60.SetActive(false);
            Health_50.SetActive(false);
            Health_40.SetActive(false);
            Health_30.SetActive(false);
            Health_20.SetActive(false);
            Health_10.SetActive(false);

            if (health.GetHealthPercent() <= 0.1)
            {
                Health_10.SetActive(true);
            }
            else if (health.GetHealthPercent() <= 0.2)
            {
                Health_20.SetActive(true);
            }
            else if (health.GetHealthPercent() <= 0.3)
            {
                Health_30.SetActive(true);
            }
            else if (health.GetHealthPercent() <= 0.4)
            {
                Health_40.SetActive(true);
            }
            else if (health.GetHealthPercent() <= 0.5)
            {
                Health_50.SetActive(true);
            }
            else if (health.GetHealthPercent() <= 0.6)
            {
                Health_60.SetActive(true);
            }
            else if (health.GetHealthPercent() <= 0.7)
            {
                Health_70.SetActive(true);
            }
            else if (health.GetHealthPercent() <= 0.8)
            {
                Health_80.SetActive(true);
            }
            else if (health.GetHealthPercent() <= 0.9)
            {
                Health_90.SetActive(true);
            }
            else if (health.GetHealthPercent() <= 1)
            {
                Health_100.SetActive(true);
            }
        }


        //anim.SetBool("ShipDamage", true);

        UpdateCompass();
    }

    public void UpdateCompass()
    {
        if (objective && controller)
        {
            var dif = objective.position - controller.transform.position;
            var degrees = Mathf.Atan2(dif.y, dif.x) * 180f / Mathf.PI;
            Compass_Stick.rotation = Quaternion.Euler(0,0, degrees - 90f);
        }
    }

    public void ToggleMessage(bool active, string text = null)
    {
        if (text != null)
        {
            message.SetText(text);
        }
        message.gameObject.SetActive(active);
    }
    
    public void ToggleMessageCost(bool active)
    {

        messageCost.SetText("You need " + GameManager.Instance.end.Cost + " coins to continue");
        messageCost.gameObject.SetActive(active);
    }

}
