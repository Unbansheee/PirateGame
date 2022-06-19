using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private State state;
    private Ship ship;
    private HealthComponent healthC;
    private Inventory inventory;

    private Ship target = null;
    private TradingPort destination = null;

    enum State
    {
        ATTACKING,
        FLEEING,
        ROAMING,
        TRADING
    }

    private void Awake()
    {
        ship = gameObject.GetComponent<Ship>();
        inventory = gameObject.GetComponent<Inventory>();
        healthC = gameObject.GetComponent<HealthComponent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case State.ATTACKING:
            {
                AttackingState();
                break;
            }
            case State.FLEEING:
            {
                FleeingState();
                break;
            }
            case State.ROAMING:
            {
                RoamingState();
                break;
            }
            case State.TRADING:
            {
                TradingState();
                break;
            }
            default:
            {
                Debug.LogWarning("Enemy entering non-handed state");
                state = State.ROAMING;
                break;
            }
        }
    }

    protected void AttackingState()
    {
        // Navmesh encircle palyer and fire side cannons
    }

    protected void FleeingState()
    {
        // Navmeseh opposide direction to player
    }
    
    protected void RoamingState()
    {
        // Roam with navmesh towards target port
    }

    protected void TradingState()
    {
        // if merchant vessel
        // Wait while 'trading' then ask for a new random port and travel
    }

    public bool CanSeePlayer()
    {
        Ship player = GameManager.Player;
        Vector2 dif = player.gameObject.transform.position - gameObject.transform.position;
        return dif.magnitude < 100f;
    }

}
