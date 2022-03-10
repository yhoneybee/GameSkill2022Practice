using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormEnemy : BaseObject
{
    [Header("WormEnemy---------------------------------------------------------------------------------------------------------------------------------")]
    public WormEnemy next;
    public WormEnemy prev;
    static float distance = 2f;

    private void Start()
    {
    }

    private void FixedUpdate()
    {
        if (!next) return;

        var dis = Vector3.Distance(next.transform.position, transform.position);
        if (dis > distance)
        {
            var dir = (next.transform.position - transform.position).normalized;
            transform.Translate(dir * ((moveSpeed * Time.fixedDeltaTime) + (dis - distance)));
        }
        else
        {

        }
    }

    public override void Shot()
    {
    }
}
