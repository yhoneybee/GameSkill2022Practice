using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ePOOL_TYPE
{
    Bullet,
    BoomEffect,
    Bacteria,
    ScoreText,
    Germ,
    End,
}

public class ObjPool : MonoBehaviour
{
    public ePOOL_TYPE poolType;

    public GameObject goOrigin;

    private List<GameObject> objects = new List<GameObject>();

    private void Awake()
    {
        K.pools[((int)poolType)] = this;
    }

    public GameObject Get()
    {
        GameObject obj = null;

        if (objects.Count == 0)
        {
            obj = Instantiate(goOrigin);
        }
        else
        {
            obj = objects[0];
            objects.RemoveAt(0);
        }

        obj.SetActive(true);
        obj.transform.SetParent(transform);

        return obj;
    }

    public T Get<T>() => Get().GetComponent<T>();

    public void Return(GameObject obj)
    {
        obj.SetActive(false);
        objects.Add(obj);
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
