using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : BaseObject
{
    [Header("BaseEnemy---------------------------------------------------------------------------------------------------------------------------------")]
    public int score;
    public int stage = 1;
    public bool isUpgrade;

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
        isUpgrade = false;
        StartCoroutine(EMove());
    }
    public abstract IEnumerator EMove();

    public override void Die(bool isBoom)
    {
        if (isUpgrade)
        {
            int add = 120 / GameManager.Instance.Stage;
            for (int i = 0; i < 360; i += add)
            {
                K.Shot(transform.position, K.Cricle(i + 90, 30).normalized, 50, damage / 2, false);
            }

            if (GameManager.Instance.Stage == 2)
            {
                for (int i = 0; i < 360; i += add)
                {
                    var pool = K.Shot<BaseBullet>(transform.position, K.Cricle(i + 45, 30).normalized, 50, damage / 2, false);
                    if (GameManager.Instance.Stage == 2)
                    {
                        pool.obj.IsSpeedIncrease = true;
                        pool.obj.increaseSpeed = 3;
                    }
                }
            }
        }
        K.PoolGet<Coin>(ePOOL_TYPE.Coin, transform.position);
        GameManager.Instance.score += score;
        base.Die(isBoom);
    }
}
