using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossObjectiveFinder : MonoBehaviour
{
    [SerializeField]
    List<EnemyAI> enemies;
    [SerializeField]
    GameObject LastObjective;

    private EnemyAI current;

    // Start is called before the first frame update
    void Awake()
    {
        foreach (EnemyAI enemy in enemies)
        {
            if (enemy)
            {
                enemy.GetComponent<HealthComponent>().OnDeath.AddListener(Redirect);
            }
        }
        if (enemies[0])
        {
            current = enemies[0];
        }
    }

    public void Redirect()
    {
        if (current && !current.GetComponent<HealthComponent>().IsDead())
        {
            return;
        }
        foreach (EnemyAI enemy in enemies)
        {
            if (enemy && !enemy.GetComponent<HealthComponent>().IsDead())
            {
                GameManager.Instance.objective = enemy.transform;
                current = enemy;
                return;
            }
        }
        GameManager.Instance.objective = LastObjective.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
