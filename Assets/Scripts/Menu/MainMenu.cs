using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        //can use either indexID --> found in build setting or name
        SceneManager.LoadSceneAsync("Camp");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
