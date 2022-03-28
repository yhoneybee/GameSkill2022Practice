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

            if (pain >= 100)
            {
                // 게임 오버 새드 엔딩
            }
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

            if (level == 10 && Stage == 1)
            {
                // 보스 소환, 뒤지면 아래 코드 실행
                //GameManager.Instance.Stage++;
                EnemyManager.Instance.isBossSpawned = true;
            }
            else if (level == 20 && Stage == 2)
            {
                // 보스 소환, 뒤지면 아래 코드 실행
                // 해피 엔딩으로 이동
                EnemyManager.Instance.isBossSpawned = true;
            }
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
