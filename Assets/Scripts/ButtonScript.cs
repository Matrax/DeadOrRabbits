using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{

    public void OnStartButtonPress()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    public void OnQuitButtonPress()
    {
        Application.Quit();
    }

    public void OnQuitGameButtonPress()
    {
        SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);
    }
}
