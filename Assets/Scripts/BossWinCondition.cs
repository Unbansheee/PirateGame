using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWinCondition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<HealthComponent>().OnDeath.AddListener(OnPlayerVictory);
    }

    public void OnPlayerVictory()
    {
        Debug.Log("Player wins!");
    }
  
}
