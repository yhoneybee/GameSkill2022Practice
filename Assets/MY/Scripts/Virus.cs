using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : BaseEnemy
{
    [Header("Virus---------------------------------------------------------------------------------------------------------------------------------")]
    public int onceCount;

    public override IEnumerator EMove()
    {
        yield return null;
    }

    public override IEnumerator EShot()
    {
        var wait = new WaitForSeconds(3);
        Vector3 pos = Vector3.zero;
        float time = 0;
        while (true)
        {
            for (int i = 0; i < 3; i++)
            {
                time += Time.deltaTime * 5;

                pos = K.Cricle(time * (i + 60), 30, transform.position);

                K.Shot(pos, K.Cricle(Random.Range(0.0f, 180.0f), 30).normalized, 150, damage, false);

                yield return K.waitPointZeroOne;
            }

            yield return wait;
        }
    }
}
