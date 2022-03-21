using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bacteria : BaseEnemy
{
    //[Header("Bacteria---------------------------------------------------------------------------------------------------------------------------------")]
    public override IEnumerator EMove()
    {
        float time = 0;
        float x = 0, y = 0;
        dir = Vector3.zero;
        while (true)
        {
            time += Time.deltaTime;

            if (time > changeFormTime)
            {
                time = 0;
                form = Random.Range(0, 3);
                x = 0;
                y = 0;
            }

            switch (form)
            {
                case 0:
                    dir = Vector3.back * 2;
                    break;
                case 1:
                    if (x == 0 && y == 0)
                    {
                        x = Random.Range(-1000, 1000);
                        y = Random.Range(-1000, 1000);
                        dir = new Vector3(x, y, -1).normalized;
                        dir *= 4;
                    }
                    break;
                case 2:
                    x = Random.Range(-1000, 1000);
                    y = Random.Range(-1000, 1000);
                    dir = new Vector3(x, y, -1).normalized;
                    dir *= 4;
                    break;
            }

            transform.Translate(dir * moveSpeed * Time.deltaTime);
            yield return K.waitPointZeroOne;
        }
    }

    public override void Shot()
    {
    }

    // Start is called before the first frame update
    protected override void OnEnable()
    {
        base.OnEnable();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
}
