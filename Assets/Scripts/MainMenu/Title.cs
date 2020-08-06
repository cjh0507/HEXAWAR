using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public string SceneToLoad;
    public GameObject helpPanel;

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            QuitHelp();
        } 
    }

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
        helpPanel.SetActive(true);
    }

    public void QuitHelp()
    {
        helpPanel.SetActive(false);
    }


    public void QuitGame() 
    {
        Application.Quit();
    }
}
