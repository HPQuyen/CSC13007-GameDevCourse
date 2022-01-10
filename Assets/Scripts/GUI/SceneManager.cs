using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager 
{
    private static SceneManager _instance;

    public static SceneManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SceneManager();
            }
            return _instance;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
