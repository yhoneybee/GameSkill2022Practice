using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float rotSpeed;

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * rotSpeed * K.DT);
        var dis = Vector3.Distance(K.player.transform.position, transform.position);
        if (dis < 15)
        {
            transform.position = Vector3.Lerp(transform.position, K.player.transform.position, K.DT * 3);
        }
        else if (dis > 300)
        {
            K.Pool(ePOOL_TYPE.Coin).Return(gameObject);
        }
        else
        {
            transform.Translate(Vector3.back * K.DT * 30);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var obj = other.GetComponent<Player>();

        if (obj)
        {
            K.Pool(ePOOL_TYPE.Coin).Return(gameObject);
            GameManager.Instance.Exp++;
        }
    }
}
