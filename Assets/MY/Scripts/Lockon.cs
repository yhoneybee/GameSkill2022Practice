using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lockon : MonoBehaviour
{
    public int Count
    {
        get => count;
        set
        {
            if (value == 0)
            {

            }
            count = value;
        }
    }
    private int count = 7;

    List<GameObject> targets = new List<GameObject>();

    public void AddTarget(GameObject goTarget)
    {
        targets.Add(goTarget);
        Count--;
    }
}
