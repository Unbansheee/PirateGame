using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterDelayTrigger : MonoBehaviour
{

    [SerializeField]
    private float delay = 0f;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Invoke("DelayedDeactivate", delay);
    }

    void DelayedDeactivate()
    {
        gameObject.SetActive(false);
    }
}
