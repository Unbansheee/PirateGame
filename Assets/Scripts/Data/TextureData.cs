using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class TextureData : UpdatableData
{
    public Color[] baseColours;
    [Range(0,1)]
    public float[] baseStartHeights;
    
    private float savedMinHeight;
    private float savedMaxHeight;
    public void ApplyToMaterial(Material material)
    {
        //material.SetColorArray("_BaseColours", baseColours);
       // material.SetFloatArray("_BaseStartHeights", baseStartHeights);
        UpdateMeshHeights(material, savedMinHeight, savedMaxHeight);
    }

    public void UpdateMeshHeights(Material material, float minHeight, float maxHeight)
    {
        savedMinHeight = minHeight;
        savedMaxHeight = maxHeight;
        
        //material.SetFloat("_MinHeight", minHeight);
        //material.SetFloat("_MaxHeight", maxHeight);
    }
}
