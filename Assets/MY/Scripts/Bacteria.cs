using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bacteria : BaseEnemy
{
    public override IEnumerator EMove()
    {
        yield return K.waitPointZeroOne;
    }

    public override IEnumerator EShot()
    {
        var poolObj = K.PoolGet<BaseBullet>(ePOOL_TYPE.EnemyBullet);
        poolObj.obj.transform.position = transform.position;
        poolObj.obj.dir = Vector3.back;
        poolObj.obj.moveSpeed = 100;
        yield return K.waitPointZeroOne;
    }
}
