using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWinActivate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.BossDefeated.AddListener(Activate);
        gameObject.SetActive(false);
    }

    private void Activate()
    {
        gameObject.SetActive(true);
    }
}
