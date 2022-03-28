using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancerCell : BaseEnemy
{
    public override IEnumerator EMove()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            var near = K.GetNearEnemy(K.enemies.FindAll(x => !x.isUpgrade), transform);
            if (near)
            {
                near.isUpgrade = true;
            }
        }
    }

    public override IEnumerator EShot()
    {
        yield return null;
    }
}
