using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public enum Type
    {
        TWIG,
        BARK,
        GRASS_BUSHELL, 
        PEBBLE,
        SPIDER_WEB,
        APHIDS,
    }

    [SerializeField]
    [Range(1,100)]
    int BaseValue;

    Texture icon;

    public static int GetBasePrice(Type type)
    {
        switch(type)
        {
            case Type.TWIG:
                return 10;
            case Type.BARK:
                return 20;
            case Type.GRASS_BUSHELL:
                return 30;
            case Type.PEBBLE:
                return 40;
            case Type.SPIDER_WEB:
                return 60;
            case Type.APHIDS:
                return 75;
            default:
                Debug.LogWarning("Missing base price for type " + type);
                return 0;
        }
    }

    public static Type[] GetAllEnums()
    {
        return (Type[])Enum.GetValues(typeof(Item.Type));
    }

}
