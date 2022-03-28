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

    //public void RandomLevelUp()
    //{
    //    LevelUp = (eLEVELUP)Random.Range(0, ((int)eLEVELUP.End));
    //}

    void Start()
    {
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
                    // ½ºÅ³ ¾È¸¸µê
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
                case eLEVELUP.End:
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
