using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterDelayTrigger : MonoBehaviour
{

    [SerializeField]
    private float delay = 0f;

    private bool triggered = false;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!triggered)
        {
            Invoke("DelayedDeactivate", delay);
            triggered = true;
        }
    }

    void DelayedDeactivate()
    {
        gameObject.SetActive(false);
    }
}
