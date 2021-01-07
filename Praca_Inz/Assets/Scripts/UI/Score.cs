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
   


    private void Start()
    {
        
        currScore = 0;
        
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
           
        SceneManager.LoadScene("WiningScene");
       
    }
}
