using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderLinker : MonoBehaviour
{
    public Slider main;
    public Slider sub;
    public Text txtValue;
    public float MaxValue
    {
        get => maxValue;
        set
        {
            maxValue = value;
            main.maxValue = maxValue;
            sub.maxValue = maxValue;
        }
    }
    public float maxValue;
    public float CurValue
    {
        get => curValue;
        set
        {
            if (value > maxValue) return;
            else if (value < 0) return;
            curValue = value;
            txtValue.text = $"{perfix} : {curValue} / {maxValue}";
        }
    }
    public float curValue;
    public string perfix;
    public bool startZero;

    private void Awake()
    {
        MaxValue = maxValue;

        if (startZero) CurValue = 0;
        else CurValue = maxValue;

        main.value = CurValue;
        sub.value = CurValue;

        StartCoroutine(ESubToMainValue());
        StartCoroutine(EMainToCurValue());
    }

    IEnumerator EMainToCurValue()
    {
        while (true)
        {
            main.value = Mathf.Lerp(main.value, CurValue, Time.deltaTime * 3);

            yield return K.waitPointZeroOne;
        }
    }


    IEnumerator ESubToMainValue()
    {
        while (true)
        {
            sub.value = Mathf.Lerp(sub.value, main.value, Time.deltaTime * 2);

            yield return K.waitPointZeroOne;
        }
    }
}
