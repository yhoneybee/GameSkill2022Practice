using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormEnemy : BaseObject
{
    [Header("WormEnemy---------------------------------------------------------------------------------------------------------------------------------")]
    public WormEnemy next;
    static float distance = 2f;

    private float sinV;

    protected override void Start()
    {
        base.Start();
    }

    private void FixedUpdate()
    {
        if (next)
        {
            var dis = Vector3.Distance(next.transform.position, transform.position);
            if (dis > distance)
            {
                var dir = (next.transform.position - transform.position).normalized;
                transform.Translate(dir * ((moveSpeed * Time.fixedDeltaTime) + (dis - distance)));
            }
        }
        else
        {
            sinV += Time.fixedDeltaTime;
            transform.Translate(new Vector3(0, Mathf.Sin(sinV) * 0.1f, -0.1f));
        }
    }

    public override void Shot()
    {
    }
}
