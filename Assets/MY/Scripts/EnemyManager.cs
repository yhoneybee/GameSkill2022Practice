using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singletone<EnemyManager>
{
    public int type;

    private void Start()
    {
        StartCoroutine(ESpawn());
    }

    private IEnumerator ESpawn()
    {
        var wait = new WaitForSeconds(30);
        while (true)
        {
            if (GameManager.Instance.stage == 1)
            {
                //type = Random.Range(0, 3);
                switch (type)
                {
                    case 0:
                        var pool = K.GetPool(ePOOL_TYPE.Bacteria);
                        for (int i = 0; i < 3; i++)
                        {
                            var obj = pool.Get<Bacteria>();
                            obj.transform.localPosition = new Vector3(Random.Range(-40.0f, 40.0f), Random.Range(-40.0f, 40.0f), 100);
                        }
                        break;
                    case 1:
                        break;
                    case 2:
                        break;
                }
            }
            else if (GameManager.Instance.stage == 2)
            {

            }
            yield return wait;
        }
    }
}
