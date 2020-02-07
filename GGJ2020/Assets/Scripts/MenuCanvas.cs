using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCanvas : MonoBehaviour
{
    public GlobalInfo info;
    public void buttonStart()
    {
        info.currentStage = 1;
        SceneManager.LoadScene("Stage" + info.currentStage);
    }

    public void ButtonQuit()
    {
        Application.Quit();
    }
}
