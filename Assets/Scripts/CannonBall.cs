using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public float speed;
    public Vector3 velocity;

    public GameObject owner;
    
    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.position = transform.position + transform.up * speed * Time.deltaTime;

    }

    
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        //if collided object is not owner
        if (col.gameObject == owner) return;

        //if hit object implements IDestroyable interface, call Destroy()
        if (col.gameObject.GetComponent<IDamageable>() != null)
        {
            
            if (col.gameObject.GetComponent<HealthComponent>().DoDamage(10))
            {
                col.gameObject.GetComponent<IDamageable>().Destroy();
            }
            
        }
        Destroy(gameObject);


    }
        
    

    void Start()
    {
        StartCoroutine(SelfDestruct());
    }
    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
