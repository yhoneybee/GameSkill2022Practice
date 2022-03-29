using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class K
{
    public static Player player;
    public static WaitForSeconds waitPointZeroOne = new WaitForSeconds(0.01f);
    public static WaitForSeconds waitDT = new WaitForSeconds(DT);
    public static ObjPool[] pools = new ObjPool[((int)ePOOL_TYPE.End)];
    public static List<BaseEnemy> enemies = new List<BaseEnemy>();
    public static CameraMove camera;
    public static string[] levelUpInfo = { "플레이어 주위를 공전하며\n자동공격하는 보조를 소환", "플레이어 공격력 증가", "플레이어 멀티샷 증가", "플레이어 최대 체력 증가", "플레이어 스킬 쿨타임 초기화", "플레이어 주위를 공전하며\n적의 공격을 방어하는\n보조를 소환", "플레이어 차지데미지 증가", "플레이어 관통 증가", "폭팔 스킬 데미지 증가" };
    public static int chargeDamage;
    public static int playerDamage;
    public static int throughCount;
    public static int boomDamage = 15;

    public static BaseEnemy GetNearEnemy(Transform trn)
    {
        return enemies.FindAll(x => x.gameObject.activeSelf && trn.gameObject != x.gameObject).OrderBy(x => Vector3.Distance(trn.position, x.transform.position)).FirstOrDefault();
    }

    public static BaseEnemy GetNearEnemy(List<BaseEnemy> list, Transform trn)
    {
        return list.FindAll(x => x.gameObject.activeSelf && trn.gameObject != x.gameObject).OrderBy(x => Vector3.Distance(trn.position, x.transform.position)).FirstOrDefault();
    }

    public static ObjPool Pool(ePOOL_TYPE type) => pools[((int)type)];

    public static (ObjPool pool, GameObject obj) PoolGet(ePOOL_TYPE type, Vector3 pos) => (pools[((int)type)], pools[((int)type)].Get(pos));
    public static (ObjPool pool, T obj) PoolGet<T>(ePOOL_TYPE type, Vector3 pos) => (pools[((int)type)], pools[((int)type)].Get<T>(pos));

    public static float DT => Time.deltaTime * timeScale;
    public static float timeScale = 1;

    public static (ObjPool pool, GameObject obj) Shot(Vector3 spawnPos, Vector3 dir, float speed, int damage, bool isShotByPlayer, int throughCount = 0)
    {
        var pool = PoolGet<BaseBullet>((isShotByPlayer ? ePOOL_TYPE.Bullet : ePOOL_TYPE.EnemyBullet), spawnPos);
        pool.obj.dir = dir;
        pool.obj.moveSpeed = speed;
        pool.obj.damage = damage;
        pool.obj.isShotByPlayer = isShotByPlayer;
        pool.obj.throughCount = throughCount;
        pool.obj.GetComponent<TrailRenderer>().Clear();

        return (pool.pool, pool.obj.gameObject);
    }

    public static (ObjPool pool, T obj) Shot<T>(Vector3 spawnPos, Vector3 dir, float speed, int damage, bool isShotByPlayer, int throughCount = 0)
    {
        var pool = Shot(spawnPos, dir, speed, damage, isShotByPlayer, throughCount);
        return (pool.pool, pool.obj.GetComponent<T>());
    }

    public static (ObjPool pool, GameObject obj) Effect(Vector3 spawnPos, Vector3 dir, float speed)
    {
        var pool = PoolGet<BaseBullet>(ePOOL_TYPE.Effect, spawnPos);
        pool.obj.dir = dir;
        pool.obj.moveSpeed = speed;
        pool.obj.damage = 0;
        pool.obj.isShotByPlayer = true;
        pool.obj.GetComponent<TrailRenderer>().Clear();

        return (pool.pool, pool.obj.gameObject);
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
                    trn.position = Vector3.MoveTowards(trn.transform.position, pos, speed * Time.deltaTime);
                    break;
                case MoveType.Lerp:
                    trn.position = Vector3.Lerp(trn.transform.position, pos, speed * Time.deltaTime);
                    break;
                case MoveType.Slerp:
                    trn.position = Vector3.Slerp(trn.transform.position, pos, speed * Time.deltaTime);
                    break;
            }
            yield return waitPointZeroOne;
        }
    }

    public static IEnumerator EKeepDistance(Transform trn, Vector3 point, float distance, float moveSpeed)
    {
        float dis = Vector3.Distance(trn.position, point);
        while (dis > distance + 0.5f || dis < distance - 0.5f)
        {
            dis = Vector3.Distance(trn.position, point);
            trn.position = Vector3.Lerp(trn.position, point + (trn.position - point).normalized * distance, moveSpeed);
            yield return waitPointZeroOne;
        }
    }

    public static float BezierCurve(float a, float b, float c, float d, float t)
    {
        float ab = Mathf.Lerp(a, b, t);
        float bc = Mathf.Lerp(b, c, t);
        float cd = Mathf.Lerp(c, d, t);

        float abbc = Mathf.Lerp(ab, bc, t);
        float bccd = Mathf.Lerp(bc, cd, t);

        return Mathf.Lerp(abbc, bccd, t);
    }

    public static IEnumerator EBezierShot(Transform spawner, int damage, int count = 12, bool onlyOnce = false)
    {
        var wait = new WaitForSeconds(0.15f);

        for (int j = 0; j < count; j++)
        {
            for (int i = 0; i < GameManager.Instance.Level; i++)
            {
                if (enemies.FindAll(x => x.gameObject.activeSelf).Count <= 0) continue;
                //PoolGet<BezierBullet>(ePOOL_TYPE.Bezier, spawner.position).obj.Init(spawner, target, 6, 3);
                var pool = PoolGet<BezierBullet>(ePOOL_TYPE.Bezier, spawner.position);
                pool.obj.GetComponent<TrailRenderer>().Clear();
                pool.obj.moveSpeed = 200;
                pool.obj.damage = damage;
                pool.obj.Init(spawner, GetNearEnemy(player.transform).transform, 9, 6);
                if (onlyOnce) break;
            }
            yield return wait;
        }
    }

    public static Vector3 Cricle(float x, float y, float radius)
    {
        return new Vector3(Mathf.Cos(x * Mathf.Deg2Rad), 0, Mathf.Sin(y * Mathf.Deg2Rad)) * radius;
    }
    public static Vector3 Cricle(float x, float y, float radius, Vector3 origin)
    {
        return Cricle(x, y, radius) + origin;
    }

    public static Vector3 Cricle(float i, float radius)
    {
        return Cricle(i, i, radius);
    }

    public static Vector3 Cricle(float i, float radius, Vector3 origin)
    {
        return Cricle(i, i, radius, origin);
    }
}
