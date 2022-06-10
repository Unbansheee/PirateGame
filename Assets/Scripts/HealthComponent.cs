using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField]
    private float health;
    [SerializeField]
    private float maxHealth;

    public bool DoDamage(float damage) // Returns true if health is 0
    {
        health = health - damage;
        health = Mathf.Clamp(health, 0, maxHealth) ;
        return health <= 0;
    }

    public bool Heal(float amount) // Returns true if full health
    {
        health = health + amount;
        health = Mathf.Clamp(health, 0, maxHealth) ;
        return IsDead();
    }

    public bool Heal() // Returns true if full health
    {
        health = maxHealth;
        return IsFullHealth();
    }

    public bool IsDead()
    {
        return health <= 0;
    }

    public bool IsFullHealth()
    {
        return health >= maxHealth;
    }

    public void SetHealth(float val)
    {
        health = val;
    }

    public void SetMaxHealth(float newMax)
    {
        maxHealth = newMax;
    }

    public float GetHealthPercent()
    {
        return health / maxHealth;
    }
    
    public float GetHealth()
    {
        return health;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
