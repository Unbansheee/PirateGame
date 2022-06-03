using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightHouse : MonoBehaviour
{
    [SerializeField]
    private List<Light2D> lights;
    [SerializeField]
    private float rotationSpeedDegrees;
    //private List<Light2D> lights2d;
    // Update is called once per frame

    private void Awake()
    {
        //lights2d = new();
        //foreach (GameObject light in lights)
        //{
        //    lights2d.Add(light.GetComponent<Light2D>());
        //}
        //Debug.Log(lights2d);
    }

    void Update()
    {
        foreach (Light2D light in lights)
        {
            light.transform.Rotate(0, 0,Time.deltaTime * rotationSpeedDegrees);
        }
        
    }
}
