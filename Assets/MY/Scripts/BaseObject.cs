using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseObject : MonoBehaviour
{
    [Header("BaseObject---------------------------------------------------------------------------------------------------------------------------------")]
    public int MaxHp = 100;
    public int Hp
    {
        get => hp;
        set
        {
            if (value > MaxHp) hp = MaxHp;
            else if (value <= 0) Die();
            else hp = value;
        }
    }
    private int hp;

    public int moveSpeed = 5;

    public int damage = 1;

    public void TakeDamage(int damage)
    {
        Hp -= damage;
    }

    public void TakeDamage(BaseObject obj) => TakeDamage(obj.damage);

    public virtual void Die()
    {
        StartCoroutine(EDie());
    }

    public abstract void Shot();

    private IEnumerator EDie()
    {
        float time = 0;
        while (Mathf.Abs(transform.localScale.x - 0) >= 0.01f)
        {
            time += Time.deltaTime;

            transform.Rotate(Vector3.one * (time + 10));

            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, 0.1f);

            yield return K.waitPointZeroOne;
        }

        Destroy(gameObject);

        yield return null;
    }
}
