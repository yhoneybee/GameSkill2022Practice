using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IHitable
{
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

    public bool IsEnemy()
    {
        return true;
    }

    public void TakeDamage(int damage)
    {
        Hp -= damage;
    }

    public void Die()
    {
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
