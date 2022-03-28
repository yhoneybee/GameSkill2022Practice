using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edon : MonoBehaviour
{
    public Player player;
    public float i;
    public float radius = 15;
    public bool isRotateClockDir;
    public bool isDefenceable;

    private void Start()
    {
        StartCoroutine(ERotation());
        StartCoroutine(EShot());
    }

    IEnumerator EShot()
    {
        while (!isDefenceable)
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
            pos = K.Cricle(i, radius, player.transform.position);

            transform.localPosition = pos;

            if (isRotateClockDir) i -= player.edonsSpeed;
            else i += player.edonsSpeed;

            yield return K.waitPointZeroOne;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isDefenceable) return;
        var bullet = other.GetComponent<BaseBullet>();
        if (bullet)
        {
            if (bullet.isShotByPlayer) return;
            K.Pool(bullet.poolType).Return(bullet.gameObject);
        }
    }
}
