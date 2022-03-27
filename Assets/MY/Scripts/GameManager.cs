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
    public int Pain
    {
        get => pain;
        set
        {
            pain = value;

            painLinker.CurValue = pain;
        }
    }
    public int pain;
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
    public int exp;
    public int Exp
    {
        get => exp;
        set
        {
            exp = value;

            if (exp >= NeedExp)
            {
                Level++;
                exp = 0;
                K.camera.IsMoving = true;
            }

            expLinker.MaxValue = NeedExp;
            expLinker.CurValue = exp;
        }
    }

    public int stage = 1;
    public int Stage
    {
        get => stage;
        set
        {
            stage = value;
            switch (stage)
            {
                case 1:
                    Pain = 10;
                    K.player.Hp = K.player.MaxHp;
                    break;
                case 2:
                    Pain = 30;
                    K.player.Hp = K.player.MaxHp;
                    break;
            }
        }
    }

    public float NeedExp => (level + 1) * 5;

    private void Start()
    {
        hpLinker.MaxValue = K.player.maxHp;
        Stage = 1;
    }

    private void FixedUpdate()
    {
        hpLinker.CurValue = K.player.Hp;
    }

    private void Update()
    {
    }
}
