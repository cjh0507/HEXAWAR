using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseUI;

    public AudioSource BGMAudioSrc;

    private static bool paused = false;


    // Start is called before the first frame update
    void Start()
    {
        PauseUI.SetActive(false);    
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            paused = !paused;
        }
        if(paused)
        {
            PauseUI.SetActive(true);
            Time.timeScale = 0f;
            if(BGMAudioSrc.isPlaying)
                BGMAudioSrc.Pause();
        }

        if(!paused)
        {
            PauseUI.SetActive(false);
            Time.timeScale = 1f;
            if(!BGMAudioSrc.isPlaying)
                BGMAudioSrc.Play();
        }
        
    }
    public void Resume() {
        paused = !paused;
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void Quit()
    {
        Application.Quit();
    }

    public static bool GetPaused() {
        return paused;
    }
}
