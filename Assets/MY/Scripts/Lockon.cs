using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lockon : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var hit = other.GetComponent<IHitable>();
        if (hit != null)
        {
            for (int i = -3; i <= 3; i++)
            {
                var bullet = K.GetPool(ePOOL_TYPE.Bullet).Get<Bullet>();
                bullet.target = other.gameObject;
                bullet.transform.position = K.player.transform.position + Vector3.right * i;
                bullet.speed = 30;
                bullet.dir = Vector2.up;
                bullet.damage = 1;
                bullet.isEnemy = false;
            }
        }
    }
}
