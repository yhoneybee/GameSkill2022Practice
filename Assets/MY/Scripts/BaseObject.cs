using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class BaseObject : MonoBehaviour
{
    [Header("BaseObject---------------------------------------------------------------------------------------------------------------------------------")]
    public ePOOL_TYPE objType;
    public int MaxHp
    {
        get => maxHp;
        set
        {
            maxHp = value;
            Hp = value;
        }
    }
    public int maxHp;
    public int Hp
    {
        get => hp;
        set
        {
            if (isNoDamage && value < hp) return;
            if (value > maxHp) hp = maxHp;
            else if (value <= 0) Die();
            else hp = value;
        }
    }
    public int hp;

    public float moveSpeed = 5;

    public int damage = 2;

    public bool isPlayer;

    public bool isNoDamage;

    public void OnEnable()
    {
        StartCoroutine(EOnEnable());
    }

    private void Awake()
    {
        GetComponent<Rigidbody>().useGravity = false;
    }

    public virtual void Die()
    {
        var poolObj = K.PoolGet(ePOOL_TYPE.Boom, transform.position);
        poolObj.pool.WaitReturn(poolObj.obj, 3);
        K.Pool(objType).Return(gameObject);
    }

    public virtual IEnumerator EOnEnable()
    {
        yield return null;
        Hp = maxHp;
        StartCoroutine(EShot());
    }

    public abstract IEnumerator EShot();

    private void OnTriggerEnter(Collider other)
    {
        var obj = other.GetComponent<BaseBullet>();
        if (obj && (isPlayer != obj.isShotByPlayer))
        {
            Hp -= obj.damage;
            if (obj.throughCount > 0)
            {
                obj.throughCount--;
                if (obj.damage > 0) obj.damage--;
            }
            else
            {
                K.Pool(obj.poolType).Return(obj.gameObject);
            }
        }
    }
}
