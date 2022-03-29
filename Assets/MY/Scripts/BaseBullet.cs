using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    [Header("BaseBullet---------------------------------------------------------------------------------------------------------------------------------")]
    public ePOOL_TYPE poolType;
    public float moveSpeed;
    public Vector3 dir;
    public int damage;
    public bool isShotByPlayer;
    public int throughCount;
    public bool isBoom;
    public float radius;

    public bool isSpeedIncrease;
    public bool IsSpeedIncrease
    {
        get => isSpeedIncrease;
        set
        {
            isSpeedIncrease = value;
            if (isSpeedIncrease) StartCoroutine(EIncrease());
        }
    }
    public float increaseSpeed;
    public float saveSpeed;

    public virtual void OnEnable()
    {
        saveSpeed = moveSpeed;
        StartCoroutine(EMove());
    }

    private void OnDisable()
    {
        var sphere = GetComponent<SphereCollider>();
        if (sphere) sphere.radius = 2;
    }

    private void FixedUpdate()
    {
        if (isBoom)
        {
        }
        else
        {
            if (Vector3.Distance(K.player.transform.position, transform.position) > 100)
            {
                dir = Vector3.zero;
                K.Pool(poolType).Return(gameObject);
            }
        }
    }

    public IEnumerator EIncrease()
    {
        while (Mathf.Abs(moveSpeed - 0) >= 0.5f)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, 0, K.DT * increaseSpeed);
            yield return K.waitPointZeroOne;
        }

        while (Mathf.Abs(moveSpeed - saveSpeed * 2) >= 0.5f)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, saveSpeed * 2, K.DT * increaseSpeed * 2);
            yield return K.waitPointZeroOne;
        }
    }

    public virtual IEnumerator EMove()
    {
        while (true)
        {
            transform.Translate(dir * K.DT * moveSpeed);

            yield return null;
        }
    }
}
