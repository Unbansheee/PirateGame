using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimation : MonoBehaviour
{
    private Animator anim;

    [SerializeField]
    private ShipControls controller;

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(controller.getGear())
        {
            case -1:
                anim.SetBool("ShipMovement-1", true);
                break;
            case 0:
                anim.SetBool("ShipMovement1", false);
                anim.SetBool("ShipMovement-1", false);//
                break;
            case 1:
                anim.SetBool("ShipMovement1", true);
                anim.SetBool("ShipMovement2", false);//
                break;
            case 2:
                anim.SetBool("ShipMovement2", true);
                anim.SetBool("ShipMovement3", false);//
                break;
            case 3:
                anim.SetBool("ShipMovement3", true);
                break;
            default:
                Debug.LogWarning("Animator cant handle gear");
                break;
        }


        //anim.SetBool("ShipDamage", true);
    }
}
