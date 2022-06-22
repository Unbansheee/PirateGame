using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;

public class DestructibleRock : MonoBehaviour, IDamageable
{
    private HealthComponent healthComponent;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    void Awake()
    {
        healthComponent = GetComponent<HealthComponent>();
        healthComponent.OnDeath.AddListener(DestroyRock);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyRock()
    {
        Destroy(gameObject);
        healthComponent.OnDeath.RemoveListener(DestroyRock);
    }


    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    public void DamageReceived(float damage)
    {

    }
}
