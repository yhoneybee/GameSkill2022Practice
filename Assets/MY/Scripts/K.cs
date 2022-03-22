using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class K
{
    public static Player player;
    public static WaitForSeconds waitPointZeroOne = new WaitForSeconds(0.01f);
    public static ObjPool[] pools = new ObjPool[((int)ePOOL_TYPE.End)];

    public static ObjPool Pool(ePOOL_TYPE type) => pools[((int)type)];

    public static (ObjPool pool, GameObject obj) PoolGet(ePOOL_TYPE type) => (pools[((int)type)], pools[((int)type)].Get());
    public static (ObjPool pool, T obj) PoolGet<T>(ePOOL_TYPE type) => (pools[((int)type)], pools[((int)type)].Get<T>());

    public static float DT => Time.deltaTime * timeScale;
    public static float timeScale = 1;
}
