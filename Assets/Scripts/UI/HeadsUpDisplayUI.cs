using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadsUpDisplayUI : MonoBehaviour
{
    public GameObject Ship_Gear_Full;
    public GameObject Ship_Gear_Full_2;
    public GameObject Ship_Gear_Full_3;
    public GameObject Ship_Gear_Reverse_Full;
    private ShipControls controller;


    // Start is called before the first frame update
    void Start()
    {
        controller = GameManager.Player.GetComponent<ShipControls>();
    }

    // Update is called once per frame
    void Update()
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


        //anim.SetBool("ShipDamage", true);
    }
}
