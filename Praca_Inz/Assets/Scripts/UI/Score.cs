using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    public static int currScore;
   
    public Text score;

    private void Start()
    {
        
        currScore = 0;
    }

    public void Update()
    {
        score.text = "Score: " + currScore.ToString();
        Win();
    }

    void Win ()
    {
        if (currScore == 5)
            SceneManager.LoadScene("WiningScene");
    }
}
