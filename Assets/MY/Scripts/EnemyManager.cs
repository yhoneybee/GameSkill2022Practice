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
    public int state;
    public bool isBossSpawned;

    private void Start()
    {
        StartCoroutine(EUpdate());
    }

    private IEnumerator EUpdate()
    {
        while (true)
        {
            if (K.DT <= 0 || isBossSpawned)
            {
                yield return null;
                continue;
            }
            yield return StartCoroutine(Stage());
            yield return new WaitForSeconds(20);
        }
    }

    private IEnumerator Stage()
    {
        state = Random.Range(0, 8);
        switch (state)
        {
            case 0:
                SpawnBacterias(5, 10);
                break;
            case 1:
                SpawnGerms(12);
                break;
            case 2:
                StartCoroutine(ESpawnVirus());
                break;
            case 3:
                SpawnGerms(6);
                StartCoroutine(ESpawnVirus());
                break;
            case 4:
                SpawnGerms(6);
                SpawnBacterias(3, 5);
                break;
            case 5:
                SpawnBacterias(3, 5);
                StartCoroutine(ESpawnVirus());
                break;
            case 6:
                SpawnGerms(3);
                SpawnGerms(3);
                SpawnGerms(3);
                K.PoolGet<CancerCell>(ePOOL_TYPE.CancerCell, new Vector3(50, 0, 30));
                K.PoolGet<CancerCell>(ePOOL_TYPE.CancerCell, new Vector3(-50, 0, 30));
                break;
            case 7:
                StartCoroutine(ESpawnVirus());
                K.PoolGet<CancerCell>(ePOOL_TYPE.CancerCell, new Vector3(50, 0, 30));
                K.PoolGet<CancerCell>(ePOOL_TYPE.CancerCell, new Vector3(-50, 0, 30));
                break;
        }
        yield return null;
    }

    private IEnumerator ESpawnVirus()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(1);
            var pool = K.PoolGet<Virus>(ePOOL_TYPE.Virus, new Vector3(0, 0, 20));
            pool.obj.onceCount = 5;
            pool.obj.MaxHp = 16 + 4 * GameManager.Instance.Level;
            pool.obj.dir = Vector3.right;
        }
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(1);
            var pool = K.PoolGet<Virus>(ePOOL_TYPE.Virus, new Vector3(0, 0, 20));
            pool.obj.onceCount = 5;
            pool.obj.MaxHp = 16 + 4 * GameManager.Instance.Level;
            pool.obj.dir = Vector3.left;
        }
    }

    private void SpawnGerms(int count, bool isChangeRadius = false)
    {
        float x = Random.Range(-100, 100);
        int add = 360 / count;
        for (int i = 0; i < 360; i += add)
        {
            var pool = K.PoolGet<Germ>(ePOOL_TYPE.Germ, new Vector3(x, 0, 50));
            pool.obj.origin = new Vector3(x, 0, 50);
            pool.obj.i = i;
            pool.obj.rotateSpeed = 0.5f;
            pool.obj.radius = 40;
            pool.obj.MaxHp = 8 + 4 * GameManager.Instance.Level;
            pool.obj.isChangeRadius = isChangeRadius;
        }
    }

    private void SpawnBacterias(int min, int max)
    {
        for (int i = 0; i < Random.Range(min + GameManager.Instance.Level, max + 1 + GameManager.Instance.Level); i++)
        {
            K.PoolGet<Bacteria>(ePOOL_TYPE.Bacteria, new Vector3(200, 0, 400)).obj.MaxHp = 4 + 4 * GameManager.Instance.Level;
        }
        for (int i = 0; i < Random.Range(min + GameManager.Instance.Level, max + 1 + GameManager.Instance.Level); i++)
        {
            K.PoolGet<Bacteria>(ePOOL_TYPE.Bacteria, new Vector3(-200, 0, 400)).obj.MaxHp = 4 + 4 * GameManager.Instance.Level;
        }
    }
}
