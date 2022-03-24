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
            state = Random.Range(0, 2);
            switch (state)
            {
                case 0:
                    for (int i = 0; i < 5; i++)
                    {
                        K.PoolGet<Bacteria>(ePOOL_TYPE.Bacteria, new Vector3(200, 0, -400)).obj.MaxHp = 4;
                    }
                    for (int i = 0; i < 5; i++)
                    {
                        K.PoolGet<Bacteria>(ePOOL_TYPE.Bacteria, new Vector3(-200, 0, -400)).obj.MaxHp = 4;
                    }
                    yield return new WaitForSeconds(20);
                    break;
                case 1:
                    for (int i = 0; i < 360; i += 30)
                    {
                        var pool = K.PoolGet<Germ>(ePOOL_TYPE.Germ, new Vector3(95, 0, 50));
                        pool.obj.origin = new Vector3(95, 0, 50);
                        pool.obj.i = i;
                        pool.obj.rotateSpeed = 0.5f;
                        pool.obj.radius = 60;
                        pool.obj.MaxHp = 8;
                        yield return K.waitPointZeroOne;
                    }
                    yield return new WaitForSeconds(30);
                    break;
            }
            yield return K.waitPointZeroOne;
        }
    }
}
