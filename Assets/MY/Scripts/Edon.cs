using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edon : MonoBehaviour
{
    public Player player;
    public float i;

    private void Start()
    {
        StartCoroutine(ERotation());
        StartCoroutine(EShot());
    }

    IEnumerator EShot()
    {
        while (true)
        {
            yield return StartCoroutine(K.EBezierShot(transform, player.damage / 2, 1, true));
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator ERotation()
    {
        Vector3 pos = Vector3.zero;
        while (true)
        {
            pos = K.Cricle(i, 15, player.transform.position);

            transform.localPosition = pos;

            i += player.edonsSpeed;

            yield return K.waitPointZeroOne;
        }
    }
}
