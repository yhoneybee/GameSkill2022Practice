using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : BaseEnemy
{
    [Header("Virus---------------------------------------------------------------------------------------------------------------------------------")]
    public int onceCount;
    public Vector3 dir;

    public override IEnumerator EMove()
    {
        float time = 0;
        while (true)
        {
            yield return null;

            time += Time.deltaTime;
            transform.position += K.DT * Mathf.Sin(time / 2) * moveSpeed * dir;
        }
    }

    public override IEnumerator EShot()
    {
        var wait = new WaitForSeconds(3);

        while (true)
        {
            yield return wait;

            for (int i = 0; i < onceCount; i++)
            {
                yield return K.waitPointZeroOne;

                for (int j = 0; j < 4; j++)
                {
                    K.Shot(transform.position, K.Cricle(45 + (i * 90), 30).normalized, 50, damage / 2, false);
                }
            }
        }
    }
}
