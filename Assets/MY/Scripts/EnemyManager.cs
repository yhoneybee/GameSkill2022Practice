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
    public SphereCollider origin;
    public int state;

    private void Start()
    {
        StartCoroutine(EUpdate());
    }

    private IEnumerator EUpdate()
    {
        while (true)
        {
            switch (GameManager.Instance.stage)
            {
                case 1:
                    Stage1();
                    break;
            }
            yield return new WaitForSeconds(20);
        }
    }

    private void Stage1()
    {
        state = Random.Range(0, 3);
        switch (state)
        {
            case 0:
                SpawnBacterias();
                break;
            case 1:
                SpawnGerms();
                break;
            case 2:

                break;
        }
    }

    private static void SpawnGerms(bool isChangeRadius = false)
    {
        float x = Random.Range(-100, 100);
        for (int i = 0; i < 360; i += 30)
        {
            var pool = K.PoolGet<Germ>(ePOOL_TYPE.Germ, new Vector3(x, 0, 50));
            pool.obj.origin = new Vector3(x, 0, 50);
            pool.obj.i = i;
            pool.obj.rotateSpeed = 0.5f;
            pool.obj.radius = 60;
            pool.obj.MaxHp = 8 * GameManager.Instance.Level;
            pool.obj.isChangeRadius = isChangeRadius;
        }
    }

    private void SpawnBacterias()
    {
        for (int i = 0; i < Random.Range(5, 10) * GameManager.Instance.stage; i++)
        {
            K.PoolGet<Bacteria>(ePOOL_TYPE.Bacteria, new Vector3(200, 0, 400)).obj.MaxHp = 4 * GameManager.Instance.Level;
        }
        for (int i = 0; i < Random.Range(5, 10) * GameManager.Instance.stage; i++)
        {
            K.PoolGet<Bacteria>(ePOOL_TYPE.Bacteria, new Vector3(-200, 0, 400)).obj.MaxHp = 4 * GameManager.Instance.Level;
        }
    }
}
