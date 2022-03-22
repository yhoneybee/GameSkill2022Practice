using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using System;

public class Player : BaseObject
{
    [Header("Player---------------------------------------------------------------------------------------------------------------------------------")]
    public GameObject[] goBodies;
    public float rateTime;

    public int Level
    {
        get => level;
        set
        {
            value %= 5;
            level = value;
        }
    }
    private int level;

    private void Awake()
    {
        K.player = this;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(ERotation());
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        transform.Translate(moveSpeed * K.DT * new Vector3(x, 0, z));
    }

    private IEnumerator ERotation()
    {
        float x = 0;
        while (true)
        {
            x = -Input.GetAxisRaw("Horizontal");
            for (int i = 0; i < goBodies.Length; i++)
            {
                var go = goBodies[i];
                go.transform.rotation = Quaternion.Lerp(go.transform.rotation, Quaternion.Euler(Vector3.forward * x * 20), Time.deltaTime * 7);
            }
            yield return K.waitPointZeroOne;
        }
    }

    public override IEnumerator EShot()
    {
        var wait = new WaitForSeconds(rateTime);
        var pool = K.Pool(ePOOL_TYPE.Bullet);
        while (true)
        {
            var obj = pool.Get<BaseBullet>();
            obj.transform.position = transform.position;
            obj.dir = Vector3.forward;
            obj.moveSpeed = 200;
            obj.damage = damage;
            yield return wait;
        }
    }
}
