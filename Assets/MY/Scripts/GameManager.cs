using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singletone<GameManager>
{
    public SliderLinker hpLinker;
    public SliderLinker painLinker;
    public SliderLinker expLinker;

    public Text txtLevel;

    public int score = 0;
    public float pain;
    public int level = 1;
    public int Level
    {
        get => level;
        set
        {
            level = value;
            txtLevel.text = $"LV. {level}";
        }
    }
    public float exp;
    public float Exp
    {
        get => exp;
        set
        {
            exp = value;

            if (exp >= NeedExp)
            {
                Level++;
                exp = 0;
            }

            expLinker.MaxValue = NeedExp;
            expLinker.CurValue = exp;
        }
    }

    public int startHp;

    public float NeedExp => (level + 1) * 5;

    private void Start()
    {
        hpLinker.MaxValue = K.player.maxHp;
    }

    private void FixedUpdate()
    {
        hpLinker.CurValue = K.player.Hp;
    }
}
