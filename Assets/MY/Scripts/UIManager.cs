using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singletone<UIManager>
{
    public Image imgFade;
    public LevelUpLinker[] levelUpLinkers;

    public IEnumerator EFade(bool fade)
    {
        float endV = fade ? 1.0f : 0.0f;

        for (int i = 0; fade && i < levelUpLinkers.Length; i++)
        {
            levelUpLinkers[i].RandomLevelUp();
        }

        while (Mathf.Abs(imgFade.fillAmount - endV) > 0.01f)
        {
            imgFade.fillAmount = Mathf.Lerp(imgFade.fillAmount, endV, Time.deltaTime);
            yield return null;
        }
        imgFade.fillAmount = endV;
    }
}
