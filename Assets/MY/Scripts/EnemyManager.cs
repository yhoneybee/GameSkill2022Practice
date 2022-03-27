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

    public int stateCount;
    public int StateCount
    {
        get => stateCount;
        set
        {
            stateCount = value;
            if (stateCount >= 15)
            {
                if (GameManager.Instance.Stage == 1)
                {
                    // 보스 소환, 뒤지면 아래 코드 실행
                    //GameManager.Instance.Stage++;
                }
                else
                {
                    // 보스 소환, 뒤지면 아래 코드 실행
                    // 해피 엔딩으로 이동
                }
            }
        }
    }

    private void Start()
    {
        StartCoroutine(EUpdate());
    }

    private IEnumerator EUpdate()
    {
        while (true)
        {
            if (K.DT <= 0)
            {
                yield return null;
                continue;
            }
            switch (GameManager.Instance.stage)
            {
                case 1:
                    yield return StartCoroutine(Stage1());
                    break;
            }
            yield return new WaitForSeconds(20);
        }
    }

    private IEnumerator Stage1()
    {
        state = Random.Range(0, 5);
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
            pool.obj.MaxHp = 16 * GameManager.Instance.Level;
            pool.obj.dir = Vector3.right;
        }
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(1);
            var pool = K.PoolGet<Virus>(ePOOL_TYPE.Virus, new Vector3(0, 0, 20));
            pool.obj.onceCount = 5;
            pool.obj.MaxHp = 16 * GameManager.Instance.Level;
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
            pool.obj.MaxHp = 8 * GameManager.Instance.Level;
            pool.obj.isChangeRadius = isChangeRadius;
        }
    }

    private void SpawnBacterias(int min, int max)
    {
        for (int i = 0; i < Random.Range(min, max + 1); i++)
        {
            K.PoolGet<Bacteria>(ePOOL_TYPE.Bacteria, new Vector3(200, 0, 400)).obj.MaxHp = 4 * GameManager.Instance.Level;
        }
        for (int i = 0; i < Random.Range(min, max + 1); i++)
        {
            K.PoolGet<Bacteria>(ePOOL_TYPE.Bacteria, new Vector3(-200, 0, 400)).obj.MaxHp = 4 * GameManager.Instance.Level;
        }
    }
}
