using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bacteria : BaseEnemy
{
    Vector3 axis;

    public override void ChangeForm()
    {
        //form = Random.Range(0, 2);
        //axis = (Random.Range(0, 2) == 0 ? Vector3.up : Vector3.down);
    }

    public override IEnumerator EMove()
    {
        yield return StartCoroutine(K.EMove(transform, new Vector3(Random.Range(-70.0f, 70.0f), 0, Random.Range(20.0f, 30.0f)), 2, 5, MoveType.Slerp));
        //yield return StartCoroutine(K.EKeepDistance(transform, Vector3.forward * 25, 30, 10 * Time.deltaTime));

        while (true)
        {
            //switch (form)
            //{
            //    case 0:
            //        transform.RotateAround(Vector3.forward * 25, axis, (moveSpeed * moveSpeed) * K.DT);
            //        break;
            //    case 1:
            //        transform.localRotation = Quaternion.identity;
            //        transform.Translate(Vector3.back * moveSpeed * K.DT);
            //        break;
            //}
            //transform.Translate(Vector3.back * moveSpeed * K.DT);
            yield return K.waitPointZeroOne;
        }
    }

    public override IEnumerator EShot()
    {
        yield return K.waitPointZeroOne;
    }
}
