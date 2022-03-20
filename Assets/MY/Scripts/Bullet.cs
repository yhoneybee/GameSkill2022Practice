using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject goChild;
    public float speed;
    public Vector3 dir;
    public string targetTag;
    public int damage;
    public GameObject target;
    public bool isGuided;

    private void OnEnable()
    {
        StartCoroutine(EUpdate());
    }

    private void OnDisable()
    {
        target = null;
        dir = Vector3.zero;
    }

    private void FixedUpdate()
    {
        if (transform.position.z > 100)
        {
            K.GetPool(ePOOL_TYPE.Bullet).Return(gameObject);
        }
    }

    private IEnumerator EUpdate()
    {
        while (true)
        {
            transform.Translate(dir * speed * Time.deltaTime * K.timeScale);
            goChild.transform.LookAt(transform.position + dir);
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
    }

    private void OnTriggerEnter(Collider other)
    {
        var obj = other.GetComponent<BaseObject>();

        if (!obj) return;
        if (!other.gameObject.CompareTag(targetTag)) return;

        obj.TakeDamage(damage);
        K.GetPool(ePOOL_TYPE.Bullet).Return(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        K.GetPool(ePOOL_TYPE.Bullet).Return(gameObject);
    }
}
