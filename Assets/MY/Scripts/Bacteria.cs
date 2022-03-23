using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bacteria : BaseEnemy
{
    public override void ChangeForm()
    {
        form = Random.Range(0, 3);
    }

    public override IEnumerator EMove()
    {
        while (true)
        {
            switch (form)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
            }
            yield return K.waitPointZeroOne;
        }
    }

    public override IEnumerator EShot()
    {
        yield return K.waitPointZeroOne;
        //while (true)
        //{
        //    K.Shot(transform.position, Vector3.back, 200, damage, false);
        //    yield return new WaitForSeconds(1);
        //}
    }
}
