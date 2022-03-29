using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singletone<UIManager>
{
    public Image imgFade;
    public LevelUpLinker[] levelUpLinkers;
    public List<int> random = new List<int>();

    public IEnumerator EFade(bool fade)
    {
        float endV = fade ? 1.0f : 0.0f;

        random.Clear();

        for (int i = 0; i < ((int)eLEVELUP.End); i++)
        {
            random.Add(i);
        }

        int idx = 0;

        for (int i = 0; fade && i < levelUpLinkers.Length; i++)
        {
            idx = Random.Range(0, random.Count);
            levelUpLinkers[i].LevelUp = (eLEVELUP)random[idx];
            random.RemoveAt(idx);
            levelUpLinkers[i].btn.enabled = true;
        }

        while (Mathf.Abs(imgFade.fillAmount - endV) > 0.01f)
        {
            imgFade.fillAmount = Mathf.Lerp(imgFade.fillAmount, endV, Time.deltaTime);
            yield return null;
        }
        imgFade.fillAmount = endV;
    }
}
