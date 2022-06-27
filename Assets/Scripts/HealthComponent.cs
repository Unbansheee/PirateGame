using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    [SerializeField]
    private float health;
    [SerializeField]
    private float maxHealth;

    [HideInInspector]
    public UnityEvent OnDamage;
    [HideInInspector]
    public UnityEvent OnDeath;


    public bool HealOverTime { get; set; } = false;

    public bool DoDamage(float damage) // Returns true if health is 0
    {
        if (health <= 0)
        {
            return true;
        }
        health = health - damage;
        health = Mathf.Clamp(health, 0, maxHealth) ;
        //call DamageReceived on IDamageable interface
        if (gameObject.GetComponent<IDamageable>() != null)
        {
            gameObject.GetComponent<IDamageable>().DamageReceived(damage);
            OnDamage.Invoke();
        }
        
        if (health <= 0)
        {
            OnDeath.Invoke();
            return true;
        }
        return false;
        
        
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
        if (health <= 0)
        {
            OnDeath.Invoke();
        }
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
        if (HealOverTime)
        {
            Heal(10f * Time.deltaTime);
        }
    }
}
