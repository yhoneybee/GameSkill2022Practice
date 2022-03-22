using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : BaseObject
{
    [Header("BaseEnemy---------------------------------------------------------------------------------------------------------------------------------")]
    public SphereCollider sphereCollider;

    public override void OnEnable()
    {
        if (!sphereCollider) sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.radius *= 3;
        base.OnEnable();
        StartCoroutine(EMove());
    }
    public abstract IEnumerator EMove();
}
