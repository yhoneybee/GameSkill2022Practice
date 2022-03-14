using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject goChild;
    public float speed;
    public Vector3 dir;
    public bool isEnemy;
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
        if (Vector3.Distance(K.player.transform.position, transform.position) >= 20)
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
        //target = null;
        //K.GetPool(ePOOL_TYPE.Bullet).Return(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        K.GetPool(ePOOL_TYPE.Bullet).Return(gameObject);
    }
}
