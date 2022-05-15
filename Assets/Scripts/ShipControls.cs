using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;


[Serializable]
public class KeyValuePair {
    public int Gear;
    public float Speed;
}
public class ShipControls : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 50.0f;

    public float speed;
    private int _currentGear = 0;
    public List<KeyValuePair> gears = new List<KeyValuePair>();
    private Dictionary<int, float> gearSpeeds = new Dictionary<int, float>();
    
    
    IEnumerator Lerp(float begin, float end, float time){
        float n = 0;  // lerped value
        for(float f = 0; f <= time; f += Time.deltaTime) {
            n = Mathf.Lerp(begin, end, f / time); // passing in the start + end values, and using our elapsed time 'f' as a portion of the total time 'x'
            // use 'n' .. ?
            speed = n;
            yield return n;
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

        var transform1 = transform;
        transform.position = transform1.position + (transform1.right * (speed * Time.deltaTime));
    }
}
