using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Germ : BaseEnemy
{
    [Header("Germ---------------------------------------------------------------------------------------------------------------------------------")]
    public Vector3 origin;
    public float rotateSpeed;
    public float i;
    public float radius;

    public override void ChangeForm()
    {
    }

    public override IEnumerator EMove()
    {
        Vector3 pos = Vector3.zero;
        while (true)
        {
            pos = K.Cricle(i, radius, origin);

            transform.position = pos;

            i += rotateSpeed;

            yield return K.waitPointZeroOne;
        }
    }

    public override IEnumerator EShot()
    {
        //Vector3 dir = Vector3.zero;
        //int i = 0;
        //while (true)
        //{
        //    i += 2;
        //    for (int j = 0; j < 360; j += 60)
        //    {
        //        dir = K.Cricle(i + j, 1);
        //        K.Shot(transform.position, dir, 30, damage, false);
        //        yield return K.waitPointZeroOne;
        //    }
        //}

        var wait = new WaitForSeconds(2);

        while (true)
        {
            var pool = K.Shot<BaseBullet>(transform.position, (K.player.transform.position - transform.position).normalized, 100, damage, false);
            //pool.obj.isSpeedIncrease = true;
            //pool.obj.increaseSpeed = 3;
            yield return wait;
        }
    }
}
