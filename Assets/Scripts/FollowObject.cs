using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public GameObject follow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (follow == null)
            return;
        Vector3 otherpos = follow.transform.position;
        transform.position = new Vector3(otherpos.x, otherpos.y, transform.position.z);
    }
}
