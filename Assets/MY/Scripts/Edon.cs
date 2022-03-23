using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edon : MonoBehaviour
{
    public Player player;
    public float i;

    private void Start()
    {
        StartCoroutine(ERotation());
    }

    IEnumerator ERotation()
    {
        Vector3 pos = Vector3.zero;
        while (true)
        {
            pos = player.transform.localPosition + new Vector3(Mathf.Cos(i * Mathf.Deg2Rad), 0, Mathf.Sin(i * Mathf.Deg2Rad)) * 20;

            transform.localPosition = pos;

            i += player.edonsSpeed;

            yield return K.waitPointZeroOne;
        }
    }
}
