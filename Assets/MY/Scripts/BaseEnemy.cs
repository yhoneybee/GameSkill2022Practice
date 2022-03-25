using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : BaseObject
{
    [Header("BaseEnemy---------------------------------------------------------------------------------------------------------------------------------")]
    public int score;

    private void FixedUpdate()
    {
        if ((-95 <= transform.position.x && transform.position.x <= 95) && transform.position.z < -60)
        {
            base.Die(false);
            GameManager.Instance.Pain++;
        }
    }

    public override IEnumerator EOnEnable()
    {
        yield return StartCoroutine(base.EOnEnable());
        sphereCollider.radius = 1;
        StartCoroutine(EMove());
    }
    public abstract IEnumerator EMove();

    public override void Die(bool isBoom)
    {
        K.PoolGet<Coin>(ePOOL_TYPE.Coin, transform.position);
        GameManager.Instance.score += score;
        base.Die(isBoom);
    }
}
