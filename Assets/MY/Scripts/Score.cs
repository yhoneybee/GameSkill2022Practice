using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public float score;
    public int originFontSize;

    public Text text;

    void Start()
    {
        StartCoroutine(EScore());
    }

    IEnumerator EScore()
    {
        while (true)
        {
            score = Mathf.Lerp(score, GameManager.Instance.score, Time.deltaTime * 5);
            text.text = $"Score : {(int)score:#,0}";
            if (Mathf.Abs(score - GameManager.Instance.score) <= 0.1f)
                score = GameManager.Instance.score;
            yield return K.waitPointZeroOne;
        }
    }
}
