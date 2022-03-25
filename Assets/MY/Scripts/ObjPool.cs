using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ePOOL_TYPE
{
    Boom,
    Bullet,
    EnemyBullet,
    Coin,
    Bacteria,
    Germ,
    Virus,
    Bezier,
    Effect,
    End,
}

public class ObjPool : MonoBehaviour
{
    public ePOOL_TYPE poolType;

    public GameObject goOrigin;

    private Queue<GameObject> readyObjects = new Queue<GameObject>();

    private void Awake()
    {
        K.pools[((int)poolType)] = this;
    }

    public GameObject Get(Vector3 pos)
    {
        GameObject obj = null;

        if (readyObjects.Count == 0)
        {
            obj = Instantiate(goOrigin);
            if (obj.CompareTag("Enemy"))
            {
                K.enemies.Add(obj.GetComponent<BaseEnemy>());
            }
        }
        else
        {
            obj = readyObjects.Dequeue();
        }

        obj.transform.SetParent(transform);
        obj.transform.position = pos;
        obj.SetActive(true);

        return obj;
    }

    public T Get<T>(Vector3 pos) => Get(pos).GetComponent<T>();

    public void Return(GameObject obj)
    {
        obj.SetActive(false);
        readyObjects.Enqueue(obj);
    }

    public void WaitReturn(GameObject obj, float time)
    {
        StartCoroutine(EWaitReturn(obj, time));
    }

    private IEnumerator EWaitReturn(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        Return(obj);
    }
}
