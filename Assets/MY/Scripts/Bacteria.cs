using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bacteria : BaseEnemy
{
    public override IEnumerator EMove()
    {
        yield return StartCoroutine(K.EMove(transform, new Vector3(Random.Range(-70.0f, 70.0f), 0, Random.Range(25.0f, 30.0f)), 2, 5, MoveType.Slerp));
        //yield return StartCoroutine(K.EKeepDistance(transform, Vector3.forward * 25, 30, 10 * Time.deltaTime));

        float time = Random.Range(0, 180);

        while (true)
        {
            time += Time.deltaTime;
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
            transform.Translate(Vector3.back * moveSpeed * K.DT * (((Mathf.Sin(time) + 1) / 2) + 1));
            yield return K.waitPointZeroOne;
        }
    }

    public override IEnumerator EShot()
    {
        yield return K.waitPointZeroOne;
    }
}
