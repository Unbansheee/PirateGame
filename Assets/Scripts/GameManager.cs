// Singleton class to access menus used accross the project

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    enum Level
    {
        NONE,
        START,
        FROGE,
    }

    [SerializeField]
    private BarterUI barterMenu;
    [SerializeField]
    private List<List<TradingPort>> ports;
    [SerializeField]
    private Ship playerShip;

    private Level currrentLevel;

    // Start is called before the first frame update
    static GameManager Instance;

    void Awake()
    {
        Instance = this;
        Instance.barterMenu = barterMenu;
        Instance.ports = ports;
        Instance.playerShip = playerShip;
        currrentLevel = Level.START;
    }

    // Returns the barter menu instance
    public static BarterUI BarterMenu { get { return Instance.barterMenu; } }

    // Returns the player ship
    public static Ship Player { get { return Instance.playerShip; } }

    // Returns a random port in the current level excluding the passed port
    // If there are no remaining ports to choose from returns null
    private TradingPort RandomPortInLevel(TradingPort exclude = null, Level level = Level.NONE)
    {
        level = level == Level.NONE ? currrentLevel : level;
        List<TradingPort> list = ports[(int)level];
        int number = list.Count;
        if (number == 0 || (!exclude && number == 1))
        {
            return null;
        }
        while (true)
        {
            int index = (int)UnityEngine.Random.Range(0, number);
            if (exclude && list[index] == exclude)
            {
                continue;
            }
            return list[index];
        }
    }

    // public static accessor for above member function
    public static TradingPort RandomPort(TradingPort exclude = null)
    {
        return Instance.RandomPortInLevel(exclude, Instance.currrentLevel);
    }

    // Returns list of ports in current level
    public static List<TradingPort> GetPortsInLevel()
    {
        return Instance.ports[(int)Instance.currrentLevel];
    }
}
