using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{

    public GameObject Cannonball;
    [SerializeField]
    public Direction cannonDirection = Direction.FORWARD;
    public Vector3 currentVelocity;
    public Vector3 prevPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentVelocity = (transform.position - prevPosition) / Time.deltaTime;
        prevPosition = transform.position;
    }

    public void Fire()
    {
        var ball = Instantiate(Cannonball, transform.position, transform.rotation);
        //apply current velocity to the ball
        ball.GetComponent<Rigidbody2D>().velocity = currentVelocity;
        ball.GetComponent<Rigidbody2D>().AddForce(transform.up * 50, ForceMode2D.Impulse);
        ball.GetComponent<CannonBall>().owner = transform.parent.parent.gameObject;

        AudioSource audio = GetComponent<AudioSource>();
        //randomize audio pitch
        audio.pitch = Random.Range(0.9f, 1.1f);
        audio.Play();
    }
}
