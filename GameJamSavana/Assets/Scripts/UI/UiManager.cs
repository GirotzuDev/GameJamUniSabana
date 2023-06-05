using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    // Start is called before the first frame update
    

    public void Restart()
    {
        Debug.Log("Restar siendo undido");
        SceneManager.LoadScene("Assets/Scenes/MainScene.unity");
    }
    public void Menu()
    {
        SceneManager.LoadScene("Assets/Scenes/Menu.unity");
    }
}
