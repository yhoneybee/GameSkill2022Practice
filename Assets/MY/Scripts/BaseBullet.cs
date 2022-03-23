using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    [Header("BaseBullet---------------------------------------------------------------------------------------------------------------------------------")]
    public float moveSpeed;
    public Vector3 dir;
    public int damage;
    public bool isShotByPlayer;

    public virtual void OnEnable()
    {
        StartCoroutine(EMove());
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(K.player.transform.position, transform.position) > 300)
        {
            K.Pool((isShotByPlayer ? ePOOL_TYPE.Bullet : ePOOL_TYPE.EnemyBullet)).Return(gameObject);
        }
    }

    private IEnumerator EMove()
    {
        while (true)
        {
            transform.Translate(dir * (isShotByPlayer ? Time.deltaTime : K.DT) * moveSpeed);

            yield return K.waitPointZeroOne;
        }
    }
}
