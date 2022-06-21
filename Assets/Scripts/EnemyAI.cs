using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public enum ShipState
{
    STATIONARY,
    ROAMING,
    ADVANCING,
    FLEEING,
    ATTACKING,
    CIRCLING
}

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private Transform waypoint;
    [SerializeField]
    private float visionRange = 30f;
    [SerializeField]
    private float circleRadius = 10f;
    [SerializeField]
    private Direction playerDirection;
    [SerializeField]
    private List<Cannon> cannons;

    IAstarAI aStarAI;
    private Transform target;
    private ShipState state = ShipState.ROAMING;
    private Ship player;
    private float playerDistance = float.PositiveInfinity;
    private float playerRadians = 0f;
    private float circleRadians = 0f;
    private bool canFire = true;
    private Vector3? positionCheck = null;

    private void Start()
    {
        player = GameManager.Player;
        SetState(ShipState.ROAMING);
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        aStarAI = GetComponent<IAstarAI>();
        if (aStarAI != null)
        {
            aStarAI.onSearchPath += Update;
        }
        waypoint.transform.parent = null;
    }

    void OnDisable()
    {
        if (aStarAI != null) aStarAI.onSearchPath -= Update;
    }

    // Update is called once per frame
    void Update()
    {
        playerDistance = (transform.position - player.transform.position).magnitude;
        playerRadians = Mathf.Atan2(player.transform.position.y, player.transform.position.x) + circleRadians;        
        switch(state)
        {
            case ShipState.STATIONARY:
                UpdateStationary();
                break;
            case ShipState.ROAMING:
                UpdateRoaming();
                break;
            case ShipState.ADVANCING:
                UpdateAdvancing();
                break;
            case ShipState.FLEEING:
                UpdateFleeing();
                break;
            case ShipState.ATTACKING:
                UpdateAttacking();
                break;
            case ShipState.CIRCLING:
                UpdateCircling();
                break;
            default:
                break;
        }
        if (target != null && aStarAI != null)
        {
            aStarAI.destination = target.position;
        }
    }

    private void UpdateStationary()
    {
        if (CanSeePlayer())
        {
            SetState(ShipState.ADVANCING);
        }
    }

    private void UpdateRoaming()
    {
        if (CanSeePlayer())
        {
            SetState(ShipState.ADVANCING);
            positionCheck = null;
        }
        else if (positionCheck == null)
        {
            positionCheck = transform.position;
            Invoke("CheckIfStuckOrAtPort", 2f);
        }
    }

    private void UpdateAdvancing()
    {
        if (!CanSeePlayer())
        {
            SetState(ShipState.ROAMING);
        }
        else if (IsPlayerInCirclingRange())
        {
            SetState(ShipState.CIRCLING);
        }
    }

    private void UpdateFleeing()
    {
        if (!CanSeePlayer())
        {
            SetState(ShipState.ROAMING);
        }
    }

    private void UpdateCircling()
    {
        if (!IsPlayerInCirclingRange())
        {
            SetState(ShipState.ADVANCING);
        }
        else
        {
            circleRadians = (circleRadians + .75f * aStarAI.maxSpeed * Time.deltaTime * circleRadius / 180f * Mathf.PI) % (2 * Mathf.PI); 
            target.position = (circleRadius * new Vector3(Mathf.Cos(playerRadians), Mathf.Sin(playerRadians), 0f)) + player.transform.position;
            if (UnityEngine.Random.Range(0, 100) < 1)
            {
                UpdateCannonDirection();
                SetState(ShipState.ATTACKING);
            }
        }
    }

    private void UpdateAttacking()
    {
        FireCannons();
        SetState(ShipState.CIRCLING);
    }

    public void SetState(ShipState state)
    {
        Debug.Log(state);
        switch (state)
        {
            case ShipState.STATIONARY:
                target = waypoint;
                break;
            case ShipState.ROAMING:
                target = GameManager.RandomPort().transform;
                break;
            case ShipState.ADVANCING:
            case ShipState.FLEEING:
                target = player.transform;
                break;
            case ShipState.CIRCLING:
                target = waypoint;
                break;
            case ShipState.ATTACKING:
                break;
        }
        this.state = state;
    }

    private bool CanSeePlayer()
    {
        return playerDistance < visionRange;
    }

    private bool IsPlayerInCirclingRange()
    {
        return playerDistance < circleRadius * 1.1f;
    }

    private void FireCannons()
    {
        if (!canFire)
            return;
        foreach (var cannon in cannons)
        {
            if (cannon.cannonDirection == playerDirection)
            {
                cannon.Fire();
            }
        }
        canFire = false;
        Invoke("ReloadCannon", UnityEngine.Random.Range(2, 4));
    }

    private void ReloadCannon()
    {
        canFire = true;
    }

    private void CheckIfStuckOrAtPort()
    {
        if (positionCheck != null && positionCheck == transform.position)
        {
            target = GameManager.RandomPort().transform;
        }
        positionCheck = null;
    }

    void UpdateCannonDirection()
    {
        Vector3 mousePosRelative = Vector3.Normalize(transform.InverseTransformPoint(player.transform.position));

        if (mousePosRelative.y > 0 && Mathf.Abs(mousePosRelative.y) > Mathf.Abs(mousePosRelative.x))
        {
            playerDirection = Direction.FORWARD;
        }
        else if (mousePosRelative.y < 0 && Mathf.Abs(mousePosRelative.y) > Mathf.Abs(mousePosRelative.x))
        {
            playerDirection = Direction.BACK;
        }
        else if (mousePosRelative.x < 0 && Mathf.Abs(mousePosRelative.y) < Mathf.Abs(mousePosRelative.x))
        {
            playerDirection = Direction.LEFT;
        }
        else if (mousePosRelative.x > 0 && Mathf.Abs(mousePosRelative.y) < Mathf.Abs(mousePosRelative.x))
        {
            playerDirection = Direction.RIGHT;
        }
    }

}
