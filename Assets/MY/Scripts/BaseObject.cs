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
            if (value < hp)
            {
                if (isNoDamage || noDamage) return;
                NoDamageTime = 1.5f;
            }
            if (value > maxHp) hp = maxHp;
            else
            {
                hp = value;
                if (hp <= 0) Die();
            }
        }
    }
    public int hp;

    public float moveSpeed = 5;

    public int damage = 2;

    public bool isPlayer;

    public bool isNoDamage;

    public bool noDamage;

    public float NoDamageTime
    {
        get => noDamageTime;
        set
        {
            noDamageDeltaTime = 0;
            noDamageTime = value - 0.5f;
        }
    }

    public float noDamageTime;

    public float noDamageDeltaTime;

    public SphereCollider sphereCollider;

    public void OnEnable()
    {
        StartCoroutine(EOnEnable());
    }

    private void Awake()
    {
        GetComponent<Rigidbody>().useGravity = false;
    }

    public virtual void Die(bool isBoom = true)
    {
        if (isBoom)
        {
            var poolObj = K.PoolGet(ePOOL_TYPE.Boom, transform.position);
            poolObj.pool.WaitReturn(poolObj.obj, 3);
        }
        if (!objType.Equals(ePOOL_TYPE.End)) K.Pool(objType).Return(gameObject);
    }

    public virtual IEnumerator EOnEnable()
    {
        yield return null;
        Hp = maxHp;
        if (!sphereCollider) sphereCollider = GetComponent<SphereCollider>();
        StartCoroutine(EShot());
        if (isPlayer) StartCoroutine(ENoDamageAlpha());
    }

    public abstract IEnumerator EShot();

    public IEnumerator ENoDamageAlpha()
    {
        var mat = K.player.matPlayer;

        while (true)
        {
            if (noDamageDeltaTime <= noDamageTime)
            {
                isNoDamage = true;

                noDamageDeltaTime += Time.deltaTime;

                mat.color = new Color(1, 1, 1, (Mathf.Sin(noDamageDeltaTime * 25) + 1) / 2);
            }
            else
            {
                mat.color = Color.white;

                if (isNoDamage) yield return new WaitForSeconds(0.5f);

                isNoDamage = false;
            }
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var bullet = other.GetComponent<BaseBullet>();
        if (bullet)
        {
            if (isPlayer == bullet.isShotByPlayer) return;
            Hp -= bullet.damage;
            if (bullet.throughCount > 0)
            {
                bullet.throughCount--;
                if (bullet.damage > 0) bullet.damage--;
            }
            else
            {
                K.Pool(bullet.poolType).Return(bullet.gameObject);
            }
        }
        else
        {
            var obj = other.GetComponent<BaseEnemy>();

            if (obj && isPlayer)
            {
                Hp -= obj.damage / 2;
            }
        }
    }
}
