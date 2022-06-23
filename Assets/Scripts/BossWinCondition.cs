using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWinCondition : MonoBehaviour
{
    // Start is called before the first frame update

    bool triggered = false;

    void Start()
    {
        GetComponent<HealthComponent>().OnDeath.AddListener(OnEnemyKilled);
        GameManager.UpdateFinalBossCount(1);
    }

    public void OnEnemyKilled()
    {
        if (!triggered)
        {
            GameManager.UpdateFinalBossCount(-1);
            triggered = true;
        }
    }

}
