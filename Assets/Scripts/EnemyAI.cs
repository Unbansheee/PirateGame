using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public enum ShipState
{
    DEAD,
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

    private HealthComponent healthC;
    IAstarAI aStarAI;
    private Transform target;
    private Ship player;
    private ShipState state = ShipState.ROAMING;
    private float playerDistance = float.PositiveInfinity;
    private float playerRadians = 0f;
    private float circleRadians = 0f;
    private float rotationDirection = 0.75f;
    private bool canFire = true;
    private Vector3? positionCheck = null;
    private bool isFleeing = false;

    private void Start()
    {
        player = GameManager.Player;
        SetState(ShipState.ROAMING);
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        healthC = GetComponent<HealthComponent>();
        healthC.OnDeath.AddListener(OnDeath);
        healthC.OnDamage.AddListener(OnDamage);
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
        if (player == null)
        {
            if (state != ShipState.ROAMING)
            {
                SetState(ShipState.ROAMING);
            }
        }
        else
        {
            playerDistance = (transform.position - player.transform.position).magnitude;
            playerRadians = Mathf.Atan2(player.transform.position.y, player.transform.position.x) + circleRadians;
        }
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
        if (!isFleeing)
        {
            isFleeing = true;
            Invoke("EndFleeing", 10f);
        }
        else if (positionCheck == null)
        {
            positionCheck = transform.position;
            Invoke("CheckIfStuckOrAtPort", 2f);
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
            circleRadians = (circleRadians + rotationDirection * aStarAI.maxSpeed * Time.deltaTime * circleRadius / 180f * Mathf.PI) % (2 * Mathf.PI); 
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
        //Debug.Log(state);
        switch (state)
        {
            case ShipState.STATIONARY:
                target = waypoint;
                break;
            case ShipState.FLEEING:
            case ShipState.ROAMING:
                target = GameManager.RandomPort().transform;
                break;
            case ShipState.ADVANCING:
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
        if (player == null || playerDistance > visionRange)
        {
            return false;
        }
        int layer_mask = LayerMask.GetMask("Islands") | LayerMask.GetMask("Player");
        RaycastHit2D hits = Physics2D.Raycast(transform.position, player.transform.position - transform.position, visionRange*2, layer_mask);
        return hits.collider && hits.collider.gameObject.CompareTag("Player");
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
        rotationDirection *= (UnityEngine.Random.Range(0, 3) == 0 ? -1f : 1f); // 25% chance to toggle rotation
        Invoke("ReloadCannon", UnityEngine.Random.Range(2, 4));
    }

    private void ReloadCannon()
    {
        canFire = true;
    }

    private void EndFleeing()
    {
        isFleeing = false;
        SetState(ShipState.ROAMING);
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

    void OnDeath()
    {
        healthC.OnDeath.RemoveListener(OnDeath);
        healthC.OnDamage.RemoveListener(OnDamage);
        SetState(ShipState.DEAD);
    }

    void OnDamage()
    {
        if (healthC.GetHealthPercent() <= 0.2f && UnityEngine.Random.Range(0,99) < 50)
        {
            SetState(ShipState.FLEEING);
        }
    }

}
