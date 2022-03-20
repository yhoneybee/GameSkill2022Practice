using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using System;

[Serializable]
public struct LevelInfo
{
    public bool[] actives;
}

public class Player : BaseObject
{
    [Header("Player---------------------------------------------------------------------------------------------------------------------------------")]
    public GameObject[] goBodies;
    public RectTransform rtrnAim;
    public GameObject[] goLunchers;
    public LevelInfo[] levelInfos;
    public float rateTime;

    public int Level
    {
        get => level;
        set
        {
            value %= 5;
            level = value;
            LevelInfo info = levelInfos[level];
            for (int i = 0; i < goLunchers.Length; i++)
            {
                goLunchers[i].SetActive(info.actives[i]);
            }
        }
    }
    private int level;

    private void Awake()
    {
        K.player = this;
    }

    protected override void Start()
    {
        base.Start();
        StartCoroutine(ERotation());

        for (int i = 0; i < goLunchers.Length; i++)
            goLunchers[i].SetActive(false);

        goLunchers[0].SetActive(true);
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        transform.Translate(moveSpeed * Time.deltaTime * new Vector3(x, z, 0));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Level++;
        }
    }

    private void FixedUpdate()
    {

    }

    public override void Die()
    {

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

    public override void Shot()
    {
    }
}
