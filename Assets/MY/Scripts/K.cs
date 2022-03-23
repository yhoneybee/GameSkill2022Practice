using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class K
{
    public static Player player;
    public static WaitForSeconds waitPointZeroOne = new WaitForSeconds(0.01f);
    public static WaitForSeconds waitDT = new WaitForSeconds(DT);
    public static ObjPool[] pools = new ObjPool[((int)ePOOL_TYPE.End)];
    public static List<BaseEnemy> enemies = new List<BaseEnemy>();

    public static ObjPool Pool(ePOOL_TYPE type) => pools[((int)type)];

    public static (ObjPool pool, GameObject obj) PoolGet(ePOOL_TYPE type, Vector3 pos) => (pools[((int)type)], pools[((int)type)].Get(pos));
    public static (ObjPool pool, T obj) PoolGet<T>(ePOOL_TYPE type, Vector3 pos) => (pools[((int)type)], pools[((int)type)].Get<T>(pos));

    public static float DT => Time.deltaTime * timeScale;
    public static float timeScale = 1;

    public static (ObjPool pool, GameObject obj) Shot(Vector3 spawnPos, Vector3 dir, float speed, int damage, bool isShotByPlayer)
    {
        var pool = PoolGet<BaseBullet>((isShotByPlayer ? ePOOL_TYPE.Bullet : ePOOL_TYPE.EnemyBullet), spawnPos);
        pool.obj.dir = dir;
        pool.obj.moveSpeed = speed;
        pool.obj.damage = damage;
        pool.obj.isShotByPlayer = isShotByPlayer;
        pool.obj.GetComponent<TrailRenderer>().Clear();

        return (pool.pool, pool.obj.gameObject);
    }

    public static (ObjPool pool, T obj) Shot<T>(Vector3 spawnPos, Vector3 dir, float speed, int damage, bool isShotByPlayer)
    {
        var pool = Shot(spawnPos, dir, speed, damage, isShotByPlayer);
        return (pool.pool, pool.obj.GetComponent<T>());
    }

    public static IEnumerator EMove(Transform trn, Vector3 pos, float speed, float loopTime, MoveType moveType)
    {
        float time = 0;
        while (Vector3.Distance(trn.transform.position, pos) > 0.1f)
        {
            time += Time.deltaTime;
            if (time > loopTime) break;
            switch (moveType)
            {
                case MoveType.MoveTowards:
                    trn.position = Vector3.MoveTowards(trn.transform.position, pos, speed * DT);
                    break;
                case MoveType.Lerp:
                    trn.position = Vector3.Lerp(trn.transform.position, pos, speed * DT);
                    break;
                case MoveType.Slerp:
                    trn.position = Vector3.Slerp(trn.transform.position, pos, speed * DT);
                    break;
            }
            yield return waitPointZeroOne;
        }
    }
}
