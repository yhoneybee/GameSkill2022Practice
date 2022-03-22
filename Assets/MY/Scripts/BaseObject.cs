using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseObject : MonoBehaviour
{
    [Header("BaseObject---------------------------------------------------------------------------------------------------------------------------------")]
    public int maxHp = 10;
    public int Hp
    {
        get => hp;
        set
        {
            if (value > maxHp) hp = maxHp;
            else if (value < 0) Die();
            else hp = value;
        }
    }
    public int hp;

    public float moveSpeed = 5;

    public int damage = 2;

    public bool isPlayer;

    public virtual void OnEnable()
    {
        Hp = maxHp;
        StartCoroutine(EShot());
    }

    public void Die()
    {
        var poolObj = K.PoolGet(ePOOL_TYPE.Boom);
        poolObj.obj.transform.position = transform.position;
        poolObj.pool.WaitReturn(poolObj.obj, 3);
    }

    public abstract IEnumerator EShot();

    private void OnTriggerEnter(Collider other)
    {
        var obj = other.GetComponent<BaseBullet>();
        if (obj && (isPlayer != obj.isShotByPlayer))
        {
            Hp -= obj.damage;
        }
    }
}
