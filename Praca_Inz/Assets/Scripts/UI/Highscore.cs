using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highscore : MonoBehaviour
{
    public int highScore;
    public int highScore1;
    public int highScore2;
    public Text highScoreT;
    public Text highScoreT1;
    public Text highScoreT2;

    void Update()
    {
        highScore = PlayerPrefs.GetInt("Highscore", 0);
        highScoreT.text = highScore.ToString();

        highScore1 = PlayerPrefs.GetInt("Highscore1", 0);
        highScoreT1.text = highScore1.ToString();

        highScore2 = PlayerPrefs.GetInt("Highscore2", 0);
        highScoreT2.text = highScore2.ToString();
    }
}
