using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScene : MonoBehaviour
{
    public void LoadMainScene()
    {
        SceneManager.LoadScene("test");
    }

    public void ExitApp()
    {
        Debug.Log("Application closed");
        Application.Quit();
    }

    public void LoadInfo()
    {
        SceneManager.LoadScene("infoMenu");
    }
}
