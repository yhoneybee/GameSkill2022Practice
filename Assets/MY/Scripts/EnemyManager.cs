using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum MoveType
{
    MoveTowards,
    Lerp,
    Slerp,
}

public class EnemyManager : Singletone<EnemyManager>
{
    public List<Vector3> wayPoints;
    public SphereCollider origin;
    public int state;

    public Vector3 GetWayPoint()
    {
        return wayPoints[Random.Range(0, wayPoints.Count)];
    }

    private void Start()
    {
        for (int y = 0; y < 2; y++)
        {
            for (int x = 1; x < 200 / 25; x++)
            {
                wayPoints.Add(new Vector3(-100 + (x * 25), 0, 50 - (y * 25)));
            }
        }
        StartCoroutine(EUpdate());
    }

    private IEnumerator EUpdate()
    {
        while (true)
        {
            switch (state)
            {
                case 0:
                    for (int i = 0; i < 5; i++)
                    {
                        K.PoolGet<Bacteria>(ePOOL_TYPE.Bacteria, new Vector3(200, 0, -400));
                    }
                    for (int i = 0; i < 5; i++)
                    {
                        K.PoolGet<Bacteria>(ePOOL_TYPE.Bacteria, new Vector3(-200, 0, -400));
                    }
                    yield return new WaitForSeconds(20);
                    break;
                case 1:
                    break;
            }

            yield return K.waitPointZeroOne;
        }
    }
}
