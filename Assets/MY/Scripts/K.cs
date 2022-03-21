using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class K
{
    public static WaitForSeconds waitPointZeroOne = new WaitForSeconds(0.01f);

    public static float timeScale = 1;

    public static Player player = null;

    public static Follow follow = null;

    public static Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

    public static ObjPool[] pools = new ObjPool[((int)ePOOL_TYPE.End)];

    public static ObjPool GetPool(ePOOL_TYPE poolType)
    {
        return pools[((int)poolType)];
    }

    public static GameObject FindChildName(string name, Transform parent)
    {
        if (parent == null) return null;

        foreach (Transform child in parent)
            if (child.name == name) return child.gameObject;

        return null;
    }

    public static T FindChildName<T>(string name, Transform parent) => FindChildName(name, parent).GetComponent<T>();
}
