using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using System;
using System.Linq;

public class Player : BaseObject
{
    [Header("Player---------------------------------------------------------------------------------------------------------------------------------")]
    public GameObject[] goBodies;
    public float rateTime;
    public Vector2 moveRange;
    public float edonsSpeed;
    public int edonsCount;
    public List<Edon> edons = new List<Edon>();
    public PlayerBulletInfo playerBulletInfo;
    public float[] theta = { 115, 85, 72 };
    public float chargingSpeed;
    public float chargeLifeTime;
    public Material matPlayer;

    int[] charges = { 0, 3, 5, 8 };

    private void Awake()
    {
        K.player = this;
        matPlayer.color = Color.white;
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

        //if (Input.GetKeyDown(KeyCode.RightShift))
        //{
        //    EdonsPosReset(++edonsCount);
        //}
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
            yield return new WaitForSeconds(rateTime);
            if (K.DT <= 0) continue;
            yield return StartCoroutine(playerBulletInfo.EShot(damage + K.playerDamage));
        }
    }

    private IEnumerator ECharge()
    {
        float time = 0;
        float i = 0;
        Vector3 pos;

        while (true)
        {
            if (K.DT > 0 && Input.GetButton("Fire1"))
            {
                time += Time.deltaTime;
                //i += 15 * (int)time + 10;
                if (time < theta.Length)
                {
                    i += theta[((int)time)];
                }
                else
                {
                    i += theta[theta.Length - 1];
                }
                if (time >= 1)
                {
                    pos = K.Cricle(i, 15, transform.position);
                    var pool = K.Effect(pos, (transform.position - pos).normalized, chargingSpeed * Time.deltaTime);
                    pool.pool.WaitReturn(pool.obj, chargeLifeTime);
                }
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                for (int j = theta.Length - 1; j >= 0; j--)
                {
                    if (time >= j)
                    {
                        StartCoroutine(K.EBezierShot(transform, damage + K.chargeDamage, charges[j]));
                        break;
                    }
                }

                time = 0;
            }

            yield return null;
        }
    }

    public void EdonsPosReset(int count)
    {
        if (count > edons.Count) return;
        for (int i = 0; i < count; i++)
        {
            edons[i].gameObject.SetActive(true);
            edons[i].i = 360 / count * i;
        }
    }
}
