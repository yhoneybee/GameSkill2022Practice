using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singletone<T> : MonoBehaviour
    where T : class
{
    public static T Instance { get; private set; } = null;

    private void Awake()
    {
        Instance = GetComponent<T>();
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}
