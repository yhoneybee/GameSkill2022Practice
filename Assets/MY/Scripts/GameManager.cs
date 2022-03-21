using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singletone<GameManager>
{
    public int stage = 1;

    public int pain;

    public int Pain
    {
        get => pain;
        set
        {
            if (value > 100)
                GameOver();
            pain = value;
        }
    }

    public int score;

    public void GameOver()
    {

    }
}
