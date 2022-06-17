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
                break;
            case 1:
                anim.SetBool("ShipMovement1", true);
                break;
            default:
                Debug.LogWarning("Animator cant handle gear");
                break;
        }


        //anim.SetBool("ShipDamage", true);
    }
}
