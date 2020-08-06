using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public string SceneToLoad;

    public void StartGame()
    {
        SceneManager.LoadScene(SceneToLoad);
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Help()
    {
        
    }


    public void QuitGame() 
    {
        Application.Quit();
    }
}
