using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;


[Serializable]
public class KeyValuePair {
    public int Gear;
    public float Speed;
}



public class ShipControls : MonoBehaviour
{

    [SerializeField] private float rotationSpeed = 200.0f;

    [SerializeField]
    private SpriteRenderer spritewings;

    public new Camera camera;
    public Vector3 mousePosRelative;
    public bool lockDirectionOnAim;
    public float speed;
    [SerializeField]
    private float rotationAmount;
    public int _currentGear = 0;
    public List<KeyValuePair> gears = new List<KeyValuePair>();
    private Dictionary<int, float> gearSpeeds = new Dictionary<int, float>();

    private Animator anim;
    
    [SerializeField]
    private Direction _mouseDirection;
    
    public List<Cannon> cannons;
    public GameObject directionIndicator;

    private bool canFire = true;

    [SerializeField]
    private float reloadTime = 1f;

    //[SerializeField] private Direction _mouseDirection;

    private Ship ship;


    void UpdateMouseDirection()
    {
        Vector3 mousePosWorld = camera.ScreenToWorldPoint(Input.mousePosition);
        mousePosWorld.z = 0;
        mousePosRelative = Vector3.Normalize(transform.InverseTransformPoint(mousePosWorld));

        if (mousePosRelative.y > 0 && math.abs(mousePosRelative.y) > math.abs(mousePosRelative.x))
        {
            _mouseDirection = Direction.FORWARD;
        }
        else if (mousePosRelative.y < 0 && math.abs(mousePosRelative.y) > math.abs(mousePosRelative.x))
        {
            _mouseDirection = Direction.BACK;
        }
        else if (mousePosRelative.x < 0 && math.abs(mousePosRelative.y) < math.abs(mousePosRelative.x))
        {
            _mouseDirection = Direction.LEFT;
        }
        else if (mousePosRelative.x > 0 && math.abs(mousePosRelative.y) < math.abs(mousePosRelative.x))
        {
            _mouseDirection = Direction.RIGHT;
        }
    }

    void Awake()
    {
        foreach (var kvp in gears)
        {
            gearSpeeds[kvp.Gear] = kvp.Speed;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        ship = GetComponent<Ship>();
    }


    // Update is called once per frame
    void Update()
    {
        if (lockDirectionOnAim)
        {
            if (Input.GetMouseButtonDown(1))
            {
                UpdateMouseDirection();
            }
        }
        else
        {
            UpdateMouseDirection();
        }
        

        directionIndicator.SetActive(Input.GetMouseButton(1));
        switch (_mouseDirection)
        {
            case Direction.FORWARD:
            {
                directionIndicator.transform.localRotation = Quaternion.Euler(0, 0, 0);
                break;
            }
            case Direction.BACK:
            {
                //directionIndicator.transform.localRotation = Quaternion.Euler(0, 0, 180);
                break;
            }
            case Direction.LEFT:
            {
                directionIndicator.transform.localRotation = Quaternion.Euler(0, 0, 90);
                break;
            }
            case Direction.RIGHT:
            {
                directionIndicator.transform.localRotation = Quaternion.Euler(0, 0, 270);
                break;
            }
        }


        if (Input.GetKey(KeyCode.A))
        {
            rotationAmount = rotationAmount + rotationSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            //rotate the object clockwise
            rotationAmount = rotationAmount + -rotationSpeed * Time.deltaTime;
        }

        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            rotationAmount = rotationAmount - (rotationAmount / 0.5f) * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            _currentGear++;
            _currentGear = math.clamp(_currentGear, -1, 3);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _currentGear--;
            _currentGear = math.clamp(_currentGear, -1, 3);
        }

        // Enter & exit trade menu
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (ship.IsTrading)
            {
                ship.EndBartering();
            }
            else if (ship.IsAtPort())
            {
                _currentGear = 0;
                ship.StartBartering();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !ship.IsTrading)
        {
            GameManager.PauseMenu.Toggle();
        }

        //Accelerate or decelerate parabolically to meet target speed
        if (speed < gearSpeeds[_currentGear])
        {
            speed += (math.abs(speed - gearSpeeds[_currentGear]) / 2) * Time.deltaTime;
        }
        else if (speed > gearSpeeds[_currentGear])
        {
            speed -= (math.abs(speed - gearSpeeds[_currentGear]) / 2) * Time.deltaTime;
        }

        //If speed is within a certain tolerance of target, round to the target speed to prevent very very long tiny numbers
        if (math.abs(speed - gearSpeeds[_currentGear]) < 0.1f)
        {
            speed = gearSpeeds[_currentGear];
        }
            
        //clamp rotationAmount to -100 to 100
        rotationAmount = math.clamp(rotationAmount, -100, 100);
        transform.Rotate(0, 0, rotationAmount * Time.deltaTime);
        
        var shipPos = transform;
        transform.position = shipPos.position + (shipPos.up * (speed * Time.deltaTime));

        
    }

    private void LateUpdate()
    {
        if (canFire && !ship.IsAtPort() && Input.GetMouseButtonDown(0) && Input.GetMouseButton(1))
        {
            foreach (var cannon in cannons)
            {
                if (cannon.cannonDirection == _mouseDirection)
                {
                    cannon.Fire();
                }
            }
            canFire = false;
            Invoke("ReloadCannon", reloadTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Islands"))
        {
            float hitAngle = Vector2.Dot(gameObject.transform.up * -1, collision.GetContact(0).normal);
            if (hitAngle > 0.8f)
            {
                if (speed > 5)
                {
                    float dmg = 10;
                    dmg = dmg * hitAngle;
                    GetComponent<HealthComponent>().DoDamage(dmg);
                }
                _currentGear = 0;
                speed = 0;
            }
        }
    }

    public int getGear()
    {
        return _currentGear;
    }


    private void ReloadCannon()
    {
        canFire = true;
        GetComponent<AudioSource>().Play();
    }

}

