using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum eSKILL_TYPE
{
    X,
    C,
    V,
}

public class Skill : MonoBehaviour
{
    public eSKILL_TYPE skillType;
    public Image imgFade;
    public Image imgFadeRot;
    public Text txtCool;
    public bool isCasting;
    public float coolDown;
    public float curCoolDown;

    public void SkillCast()
    {
        if (isCasting) return;
        curCoolDown = coolDown;

        switch (skillType)
        {
            case eSKILL_TYPE.X:
                for (int i = 0; i <= 120; i += 40)
                {
                    var pool = K.Shot<BaseBullet>(K.player.transform.position, K.Cricle(i + 30, 30).normalized, 200, K.player.damage, true, K.throughCount);
                    pool.obj.isBoom = true;
                }
                break;
            case eSKILL_TYPE.C:
                break;
            case eSKILL_TYPE.V:
                break;
        }

        StartCoroutine(ESkillCast());
    }

    public IEnumerator ESkillCast()
    {
        isCasting = true;

        imgFadeRot.fillAmount = 1;
        imgFade.fillAmount = 1;

        while (Mathf.Abs(curCoolDown - 0) >= 0.01f)
        {
            yield return null;

            curCoolDown = Mathf.MoveTowards(curCoolDown, 0, 1 * K.DT);
            txtCool.text = $"{curCoolDown}";
            imgFadeRot.fillAmount = Mathf.InverseLerp(0, coolDown, curCoolDown);
        }

        txtCool.text = "";

        imgFade.fillAmount = 0;

        isCasting = false;
    }
}
