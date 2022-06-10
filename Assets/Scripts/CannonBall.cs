using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public float speed;
    
    //public Collider collider;
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = transform.position + transform.up * speed * Time.deltaTime;

    }
    

    private void OnCollisionEnter2D(Collision2D col)
    {
        print(col.gameObject.name);

        if (col.gameObject.GetComponent<HealthComponent>() != null)
        {
            col.gameObject.GetComponent<HealthComponent>().DoDamage(10);
            Destroy(gameObject);
        }


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
