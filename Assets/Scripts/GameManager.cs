// Singleton class to access menus used accross the project

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    HeadsUpDisplayUI gui;
    [SerializeField]
    private Ship playerShip;
    [SerializeField]
    private PauseMenu pauseMenu;
    [SerializeField]
    private List<TradingPort> LevelPorts01;
    [SerializeField]
    public Transform objective;

    public LevelTarget end;

    private Level currrentLevel;

    private int finalBossCount = 0;

    public UnityEvent BossDefeated;
   
    public static UnityEvent BossDefeatedEvent {get {return Instance.BossDefeated;} }

    // Start is called before the first frame update
    public static GameManager Instance;
    //private List<List<TradingPort>> ports;

    void Awake()
    {
        Instance = this;
        currrentLevel = Level.START;
    }

    // Returns the barter menu instance
    public static BarterUI BarterMenu { get { return Instance.barterMenu; } }

    // Returns the player ship
    public static Ship Player { get { return Instance.playerShip; } }

    public static HeadsUpDisplayUI GUI { get { return Instance.gui; } }

    public static PauseMenu PauseMenu { get { return Instance.pauseMenu; } }

    public static Transform Objective { get { return Instance.objective; } }

    // Returns a random port in the current level excluding the passed port
    // If there are no remaining ports to choose from returns null
    private TradingPort RandomPortInLevel(TradingPort exclude = null, Level level = Level.NONE)
    {
        level = level == Level.NONE ? currrentLevel : level;
        int number = LevelPorts01.Count;
        if (number == 0 || (!exclude && number == 1))
        {
            return null;
        }
        while (true)
        {
            int index = (int)UnityEngine.Random.Range(0, number);
            if (exclude && LevelPorts01[index] == exclude)
            {
                continue;
            }
            return LevelPorts01[index];
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
        return Instance.LevelPorts01;
    }

    public static void UpdateFinalBossCount(int count)
    {
        Instance.finalBossCount += count;
        if (Instance.finalBossCount == 0)
        {
            Instance.BossDefeated.Invoke();
        }
    }

}
