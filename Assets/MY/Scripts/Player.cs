using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IHitable
{
    public void TakeDamage(int damage);
    public bool IsEnemy();
}

public class Player : MonoBehaviour, IHitable
{
    [Header("---------------------------------------------------------------------------------------------------------------------------------")]
    public GameObject[] goBodies;
    [Header("---------------------------------------------------------------------------------------------------------------------------------")]
    public RectTransform rtrnAim;
    public BoxCollider confiner;
    [Header("---------------------------------------------------------------------------------------------------------------------------------")]
    [Range(0f, 10f)]
    public float clampFrontMoveValue = 1;
    [Range(0f, 2f)]
    public float divValue = 1;
    [Range(0f, 100f)]
    public float aimDistance = 30;

    [Range(0, 1000)]
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

    public int Damage
    {
        get => damage;
        set => damage = value;
    }
    private int damage = 1;

    [Range(1, 10)]
    public int moveSpeed = 5;

    public bool isNoDamage;

    void Start()
    {
        StartCoroutine(ERotation());
        StartCoroutine(EShot());

        K.player = this;
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        transform.Translate(moveSpeed * Time.deltaTime * new Vector3(x, 0, z));

        var size = confiner.size / divValue;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -size.x, size.x), transform.position.y, Mathf.Clamp(transform.position.z, -size.z + clampFrontMoveValue, size.z + clampFrontMoveValue));

        var screen = Camera.main.WorldToScreenPoint(transform.position);
        rtrnAim.localPosition = new Vector3(screen.x - Screen.width / 2, screen.y - aimDistance, 0);
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

    private IEnumerator EShot()
    {
        while (true)
        {
            //var bullet = K.GetPool(ePOOL_TYPE.Bullet).Get<Bullet>();
            //bullet.transform.position = transform.position;
            //bullet.speed = 30;
            //bullet.dir = Vector2.up;
            //bullet.damage = 1;
            //bullet.isEnemy = IsEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void Die()
    {
    }

    public void NoDamageStart()
    {
        isNoDamage = true;
    }

    public void NoDamageEnd()
    {
        isNoDamage = false;
    }

    public void TakeDamage(int damage)
    {
        if (isNoDamage) return;

        Hp -= damage;

        // Anim Notify에서 위에 함수 호출하기
    }

    public bool IsEnemy()
    {
        return false;
    }
}
