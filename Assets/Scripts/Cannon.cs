using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{

    public GameObject Cannonball;
    [SerializeField]
    public Direction cannonDirection = Direction.FORWARD;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire()
    {
        Instantiate(Cannonball, transform.position, transform.rotation);
    }
}
