// Singleton class to access menus used accross the project

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private BarterUI barterMenu;

    // Start is called before the first frame update

    static GameManager Instance;

    void Awake()
    {
        Instance = this;
        Instance.barterMenu = barterMenu;
    }

    //void Start()
    //{
    //    Instance = new();
    //    Instance.barterMenu = barterMenu;
    //}
    public static BarterUI BarterMenu { get { return Instance.barterMenu; } }
}
