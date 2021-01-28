using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private float elapsedTime = 0;
    public int currScore;
    public static int enemyScore;
    public int winingEnemyScore;
    public Text score;
    Scene scene;
    public string levelToLoad;
   


    private void Start()
    {
        
        currScore = 0;
        PlayerPrefs.SetInt("GameScore", 0);


    }

    public void Update()
    {
        elapsedTime += Time.deltaTime;
        AddPoints();
        score.text = "Score: " + currScore.ToString();
        Win();
    }

    void AddPoints()
    {
        if (elapsedTime <= 5f)
        {
            currScore = Mathf.RoundToInt(1000 / (elapsedTime/10));
        }
        else if (elapsedTime > 5 && elapsedTime<= 15)
        {
            currScore = Mathf.RoundToInt(600 / (elapsedTime/10));
        }
        else
        {
            currScore = Mathf.RoundToInt(200 / (elapsedTime/10));
        }
    }

    
    void Win ()
    {

        if (enemyScore == winingEnemyScore)
        {
            PlayerPrefs.SetInt("GameScore", currScore + PlayerPrefs.GetInt("GameScore", 0));
            int temp = PlayerPrefs.GetInt("Highscore", 0);
            int temp1 = PlayerPrefs.GetInt("Highscore1", 0);
            int temp2 = PlayerPrefs.GetInt("Highscore2", 0);
            if (PlayerPrefs.GetInt("GameScore", 0) > temp)
            {
                PlayerPrefs.SetInt("Highscore", PlayerPrefs.GetInt("GameScore", 0));
                PlayerPrefs.SetInt("Highscore1", temp);
                PlayerPrefs.SetInt("Highscore2", temp1);


            }
            if (PlayerPrefs.GetInt("GameScore", 0) > temp2 && PlayerPrefs.GetInt("GameScore", 0) > temp1 && PlayerPrefs.GetInt("GameScore", 0) < temp)
            {
                PlayerPrefs.SetInt("Highscore1", PlayerPrefs.GetInt("GameScore", 0));
                PlayerPrefs.SetInt("Highscore2", temp1);

            }
            if (PlayerPrefs.GetInt("GameScore", 0) > temp2 && PlayerPrefs.GetInt("GameScore", 0) < temp && PlayerPrefs.GetInt("GameScore", 0) < temp1)
            {
                PlayerPrefs.SetInt("Highscore2", PlayerPrefs.GetInt("GameScore", 0));

            }
            if(levelToLoad == "WiningScene")
            {
                PlayerPrefs.SetInt("GameScore", 0);
            }
            SceneManager.LoadScene(levelToLoad);
        
            currScore = 0;
            enemyScore = 0;
        }
    }
}
