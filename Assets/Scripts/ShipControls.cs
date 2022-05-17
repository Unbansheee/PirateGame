using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;


[Serializable]
public class KeyValuePair {
    public int Gear;
    public float Speed;
}

enum Direction
{
    LEFT,
    RIGHT,
    FORWARD,
    BACK
}

public class ShipControls : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 50.0f;

    public float speed;
    private int _currentGear = 0;
    public List<KeyValuePair> gears = new List<KeyValuePair>();
    private Dictionary<int, float> gearSpeeds = new Dictionary<int, float>();
    
    [SerializeField]
    private Direction _mouseDirection;
    
    IEnumerator Lerp(float begin, float end, float time){
        float n = 0;  // lerped value
        for(float f = 0; f <= time; f += Time.deltaTime) {
            n = Mathf.Lerp(begin, end, f / time); // passing in the start + end values, and using our elapsed time 'f' as a portion of the total time 'x'
            speed = n;
            yield return n;
        }
    }


    void UpdateMouseDirection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        var mousePosWorld = hit.point;
        var mousePosRelative = transform.InverseTransformPoint(mousePosWorld);
        if (mousePosRelative.x < transform.localPosition.x && math.abs(mousePosRelative.y) < math.abs(mousePosRelative.x))
        {
            _mouseDirection = Direction.LEFT;
        }
        else if (mousePosRelative.x > transform.localPosition.x && math.abs(mousePosRelative.y) < math.abs(mousePosRelative.x))
        {
            _mouseDirection = Direction.RIGHT;
        }
        else if (mousePosRelative.y > transform.localPosition.y)
        {
            _mouseDirection = Direction.FORWARD;
        }
        else if (mousePosRelative.y < transform.localPosition.y)
        {
            _mouseDirection = Direction.BACK;
        }
    }
    
    void Awake() {
        foreach (var kvp in gears) {
            gearSpeeds[kvp.Gear] = kvp.Speed;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMouseDirection();
        if(Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
        }
        if(Input.GetKeyDown(KeyCode.W))
        {
            _currentGear++;
            _currentGear = math.clamp(_currentGear, -1, 3);
            StartCoroutine(Lerp(speed, gearSpeeds[_currentGear], 2));
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            _currentGear--;
            _currentGear = math.clamp(_currentGear, -1, 3);
            StartCoroutine(Lerp(speed, gearSpeeds[_currentGear], 2));
        }

        var shipPos = transform;
        transform.position = shipPos.position + (shipPos.up * (speed * Time.deltaTime));
        
        
    }
}

