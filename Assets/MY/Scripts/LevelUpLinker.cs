using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum eLEVELUP
{
    EdonAdd,
    DamageAdd,
    MutiAdd,
    MaxHpAdd,
    SkillCoolReset,
    ShieldAdd,
    ChargeDamageAdd,
    ThroughAdd,
    BoomDamageAdd,
    ChargeThroughAdd,
    LaserDamageAdd,
    OverloadingTimeAdd,
    OverloadingRateAdd,
    OverloadingDamageAdd,
    End,
}

public class LevelUpLinker : MonoBehaviour
{
    public eLEVELUP levelUp;
    public eLEVELUP LevelUp
    {
        get => levelUp;
        set
        {
            levelUp = value;
            txt.text = K.levelUpInfo[((int)levelUp)];
        }
    }
    public Button btn;
    public Text txt;

    void Start()
    {
        btn.enabled = false;
        btn.onClick.AddListener(() => 
        {
            switch (levelUp)
            {
                case eLEVELUP.EdonAdd:
                    K.player.EdonsPosReset(++K.player.edonsCount);
                    break;
                case eLEVELUP.DamageAdd:
                    ++K.playerDamage;
                    break;
                case eLEVELUP.MutiAdd:
                    ++K.player.playerBulletInfo.multi;
                    break;
                case eLEVELUP.MaxHpAdd:
                    K.player.maxHp += 50;
                    GameManager.Instance.hpLinker.MaxValue = K.player.MaxHp;
                    break;
                case eLEVELUP.SkillCoolReset:
                    for (int i = 0; i < 3; i++)
                    {
                        K.player.skills[i].curCoolDown = 0;
                    }
                    break;
                case eLEVELUP.ShieldAdd:
                    K.player.ShieldsPosReset(++K.player.shieldsCount);
                    break;
                case eLEVELUP.ChargeDamageAdd:
                    ++K.chargeDamage;
                    break;
                case eLEVELUP.ThroughAdd:
                    ++K.throughCount;
                    break;
                case eLEVELUP.BoomDamageAdd:
                    ++K.boomDamage;
                    break;
                case eLEVELUP.ChargeThroughAdd:
                    ++K.chargeThroughCount;
                    break;
                case eLEVELUP.LaserDamageAdd:
                    ++K.laserDamage;
                    break;
            }

            StartCoroutine(UIManager.Instance.EFade(false));

            K.camera.IsMoving = false;
        });
    }

    void Update()
    {
        
    }
}
