using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    //public GameObject soundOptionsUI;
    //public GameObject generalOptionsUI;
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        Debug.Log("Pausa");
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Home()
    {
        SceneManager.LoadScene("InitialMenuScene");
    }
    
    public void ShowSoundOptions()
    {
        //generalOptionsUI.SetActive(false);
        //soundOptionsUI.SetActive(true);
    }

    public void HideSoundOptions()
    {
        //generalOptionsUI.SetActive(true);
        //soundOptionsUI.SetActive(false);
    }

}
