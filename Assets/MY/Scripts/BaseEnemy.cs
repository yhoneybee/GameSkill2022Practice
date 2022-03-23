using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : BaseObject
{
    [Header("BaseEnemy---------------------------------------------------------------------------------------------------------------------------------")]
    public SphereCollider sphereCollider;
    public int form;
    public float changeFromTime;
    public float time;
    public int idx;

    private void Update()
    {
        time += Time.deltaTime;
        if (time > changeFromTime)
        {
            time = 0;
            ChangeForm();
        }
    }

    public abstract void ChangeForm();

    public override IEnumerator EOnEnable()
    {
        yield return StartCoroutine(base.EOnEnable());
        if (!sphereCollider) sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.radius *= 3;
        StartCoroutine(EMove());
    }
    public abstract IEnumerator EMove();

    public override void Die()
    {
        K.PoolGet<Coin>(ePOOL_TYPE.Coin, transform.position);
        base.Die();
    }
}
