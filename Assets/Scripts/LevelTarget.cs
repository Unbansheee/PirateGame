using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class LevelTarget : MonoBehaviour
{
    public int Cost;
    public bool RefreshLight = false;
    public SceneAsset NextScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnValidate()
    {
        //for each point in the collider, set the light shape to be a point
        int index = 0;
        foreach (var point in GetComponent<PolygonCollider2D>().points)
        {
            GetComponent<Light2D>().shapePath[index] = point;
            index++;
        }
        RefreshLight = false;
    }
    

    //load next scene on overlap
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print(("Loading next level"));
            if (collision.gameObject.GetComponent<Inventory>().GetCoins() >= Cost)
            {
                SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
                SceneManager.LoadScene(NextScene.name);
                
            }
        }
    }
    
}
