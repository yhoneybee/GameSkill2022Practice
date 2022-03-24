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
    public int score;

    private void Update()
    {
        time += Time.deltaTime;
        if (time > changeFromTime)
        {
            time = 0;
            ChangeForm();
        }
    }

    private void FixedUpdate()
    {
        //if (transform.position.z < K.player.transform.position.z - 10)
        //{
        //    base.Die();
        //}
    }

    public abstract void ChangeForm();

    public override IEnumerator EOnEnable()
    {
        yield return StartCoroutine(base.EOnEnable());
        if (!sphereCollider) sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.radius *= 2;
        StartCoroutine(EMove());
    }
    public abstract IEnumerator EMove();

    public override void Die()
    {
        K.PoolGet<Coin>(ePOOL_TYPE.Coin, transform.position);
        GameManager.Instance.score += score;
        base.Die();
    }
}
