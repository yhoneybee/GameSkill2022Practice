using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class BaseObject : MonoBehaviour
{
    [Header("BaseObject---------------------------------------------------------------------------------------------------------------------------------")]
    public ePOOL_TYPE objType;

    public int maxHp = 20;
    public int Hp
    {
        get => hp;
        set
        {
            if (isNoDamage && value < hp) return;
            if (value > maxHp) hp = maxHp;
            else if (value < 0) Die();
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

    public void Die()
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
            K.Pool((obj.isShotByPlayer ? ePOOL_TYPE.Bullet : ePOOL_TYPE.EnemyBullet)).Return(obj.gameObject);
        }
    }
}
