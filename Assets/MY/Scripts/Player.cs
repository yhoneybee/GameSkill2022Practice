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
    public Vector2 moveRange;
    public int exp;
    public float edonsSpeed;
    public List<Edon> edons = new List<Edon>();

    public int UpgradeLevel
    {
        get => upgradeLevel;
        set
        {
            value %= 5;
            upgradeLevel = value;
        }
    }
    private int upgradeLevel;

    private void Awake()
    {
        K.player = this;
        EdonsPosReset();
    }

    public override IEnumerator EOnEnable()
    {
        yield return StartCoroutine(base.EOnEnable());
        StartCoroutine(ERotation());
        StartCoroutine(ECharge());
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        transform.Translate(moveSpeed * K.DT * new Vector3(x, 0, z));

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -moveRange.x, moveRange.x), transform.position.y, Mathf.Clamp(transform.position.z, -moveRange.y, moveRange.y));
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
        while (true)
        {
            K.Shot(transform.position, Vector3.forward, 200, damage, true);
            yield return new WaitForSeconds(rateTime);
        }
    }

    private IEnumerator ECharge()
    {
        float time = 0;
        while (true)
        {
            if (Input.GetButton("Fire1"))
            {
                time += Time.deltaTime;
            }
            else
            {

                time = 0;
            }

            yield return K.waitPointZeroOne;
        }
    }

    public void EdonsPosReset()
    {
        for (int i = 0; i < edons.Count; i++)
        {
            edons[i].i = 360 / edons.Count * i;
        }
    }
}
