using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TileAutomata))]
public class CellAutomataGUI : Editor
{
    public string saveName = "Save Name";
    public override void OnInspectorGUI()
    {
        
        TileAutomata tileGen = (TileAutomata)target;

        DrawDefaultInspector();

        if (GUILayout.Button("Step Simulation"))
        {
            tileGen.doSim(tileGen.numRepetitions);
        }
        
        if (GUILayout.Button("Reset Simulation"))
        {
            tileGen.clearMap(true);
        }
        
        saveName = GUILayout.TextField(saveName);
        
        if (GUILayout.Button("Save"))
        {
            tileGen.SaveAssetMap(saveName);
        }
    }
}
