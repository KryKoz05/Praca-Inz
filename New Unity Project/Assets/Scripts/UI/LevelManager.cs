using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void Load(string name)
    {
        
        SceneManager.LoadScene(name);
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }
}
