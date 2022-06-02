using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class TileAutomata : MonoBehaviour
{

    [Range(0,100)]
    public int initChance;

    [Range(0, 8)]
    public int birthLimit;

    [Range(0, 8)]
    public int deathLimit;

    [Range(1, 10)]
    public int numRepetitions;
    
    private int[,] terrainMap;
    public Vector3Int tmapSize;

    public Tilemap topMap;
    public Tile topTile;

    private int width;
    private int height;

    public void doSim(int numR)
    {
        clearMap(false);
        width = tmapSize.x;
        height = tmapSize.y;

        if (terrainMap == null)
        {
            terrainMap = new int[width, height];
            initPos();
        }

        for (int i = 0; i < numR; i++)
        {
            terrainMap = genTilePos(terrainMap);
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (terrainMap[x, y] == 1)
                {
                    topMap.SetTile(new Vector3Int(-x + width/2, -y + height / 2, 0), topTile);
                }
            }
        }
    }

    public int[,] genTilePos(int[,] oldMap)
    {
        int[,] newMap = new int[width,height];
        int neighb;
        BoundsInt myB = new BoundsInt(-1, -1, 0, 3, 3, 1);


        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                neighb = 0;
                foreach (var b in myB.allPositionsWithin)
                {
                    if (b.x == 0 && b.y == 0) continue;
                    if (x+b.x >= 0 && x+b.x < width && y+b.y >= 0 && y+b.y < height)
                    {
                        neighb += oldMap[x + b.x, y + b.y];
                    }
                    else
                    {
                        neighb++;
                    }
                }

                if (oldMap[x,y] == 1)
                {
                    if (neighb < deathLimit) newMap[x, y] = 0;

                    else
                    {
                        newMap[x, y] = 1;

                    }
                }

                if (oldMap[x,y] == 0)
                {
                    if (neighb > birthLimit) newMap[x, y] = 1;

                    else
                    {
                        newMap[x, y] = 0;
                    }
                }

            }

        }



        return newMap;
    }
    public void initPos()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                terrainMap[x, y] = Random.Range(1, 101) < initChance ? 1 : 0;
            }
        }
    }
    
    // Update is called once per frame
    void Update()
    {

    }


    public void SaveAssetMap(string saveName)
    {
        saveName = "tmapXY_" + saveName;
        var mf = GameObject.Find("Grid");

        if (mf)
        {
            var savePath = "Assets/" + saveName + ".prefab";
            if (PrefabUtility.SaveAsPrefabAsset(mf, savePath))
            {
                EditorUtility.DisplayDialog("Tilemap Saved", "Saved under" + savePath, "Continue");
            }
            else
            {
                EditorUtility.DisplayDialog("Tilemap NOT Saved", "Error occured trying to save to" + savePath , "Continue");
            }
        }
    }
    
    public void clearMap(bool complete)
    {
        topMap.ClearAllTiles();

        if (complete)
        {
            terrainMap = null;
            
        }
    }
}
