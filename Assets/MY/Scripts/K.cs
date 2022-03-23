using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class K
{
    public static Player player;
    public static WaitForSeconds waitPointZeroOne = new WaitForSeconds(0.01f);
    public static WaitForSeconds waitDT = new WaitForSeconds(DT);
    public static ObjPool[] pools = new ObjPool[((int)ePOOL_TYPE.End)];

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

        return (pool.pool, pool.obj.gameObject);
    }

    public static (ObjPool pool, T obj) Shot<T>(Vector3 spawnPos, Vector3 dir, float speed, int damage, bool isShotByPlayer)
    {
        var pool = Shot(spawnPos, dir, speed, damage, isShotByPlayer);
        return (pool.pool, pool.obj.GetComponent<T>());
    }
}
