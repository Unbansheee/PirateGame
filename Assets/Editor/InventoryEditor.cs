using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(Inventory))]
public class InventoryEditor : Editor
{
    private Inventory script;

    public void OnEnable()
    {
        script = (Inventory)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();


        if (GUILayout.Button("Randomize"))
        {
            int coins = (int) (100 + UnityEngine.Random.value * 200);
            script.SetCoins(coins);
            script.AddMissingItems();
            script.RandomizePort();
        }
    }

}
