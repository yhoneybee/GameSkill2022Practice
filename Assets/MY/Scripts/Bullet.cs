using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public Vector2 dir;
    public bool isEnemy;
    public int damage;
    public GameObject target;
    public bool isGuided;

    private void OnEnable()
    {
        transform.LookAt(transform.position + new Vector3(dir.x, 0, dir.y));
        StartCoroutine(EUpdate());
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(K.player.transform.position, transform.position) >= 100)
        {
            target = null;
            K.GetPool(ePOOL_TYPE.Bullet).Return(gameObject);
        }
    }

    private IEnumerator EUpdate()
    {
        while (true)
        {
            transform.Translate(transform.forward * speed * Time.deltaTime * K.timeScale);
            if (target)
            {
                if (isGuided)
                {

                }
                else
                {
                    transform.LookAt(target.transform);
                }
            }
            yield return K.waitPointZeroOne;
        }

        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        var hit = other.GetComponent<IHitable>();
        if (hit != null && hit.IsEnemy() != isEnemy)
        {
            hit.TakeDamage(damage);
            target = null;
            K.GetPool(ePOOL_TYPE.Bullet).Return(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        target = null;
        K.GetPool(ePOOL_TYPE.Bullet).Return(gameObject);
    }
}
