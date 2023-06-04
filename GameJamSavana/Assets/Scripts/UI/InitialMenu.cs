using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InitialMenu : MonoBehaviour
{
    public GameObject optionsPanel;
    public GameObject creditsPanel;
    public GameObject mainPanel;
    public Slider volumeSlider;

    private void Start()
    {
        // Cargar el valor actual del volumen guardado en PlayerPrefs
        if (PlayerPrefs.HasKey("Volume"))
        {
            float savedVolume = PlayerPrefs.GetFloat("Volume");
            volumeSlider.value = savedVolume;
            UpdateVolume(savedVolume);
        }
    }

    public void PlayGame()
    {
        // Cargar la escena del juego principal
        SceneManager.LoadScene("IntegrationTestScene");
    }

    public void ShowOptions()
    {
        mainPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void HideOptions()
    {
        optionsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void ShowCredits()
    {
        mainPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void HideCredits()
    {
        creditsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void UpdateVolume(float volume)
    {
        // Actualizar el volumen de los sonidos y música del juego
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
    }
}
