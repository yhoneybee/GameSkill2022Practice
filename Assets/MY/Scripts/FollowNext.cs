using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowNext : MonoBehaviour
{
    public FollowNext next;
    public float distance;
    public float followNextSpeed;

    private void FixedUpdate()
    {
        if (next && next.gameObject.activeSelf)
        {
            var dis = Vector3.Distance(next.transform.position, transform.position);
            if (dis > distance)
            {
                var dir = (next.transform.position - transform.position).normalized;
                transform.Translate(dir * ((followNextSpeed * Time.fixedDeltaTime) + (dis - distance)));
            }
        }
    }
}
