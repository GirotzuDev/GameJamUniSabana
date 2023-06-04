using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private List<Planet> planetList; 
    public int planetLess;
    public GameObject winPanel;
    public GameObject losePanel;

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    public GameStates gameStates;

    private void Awake()
    {
        if(instance != null)
        {
            if(instance != this)
            {
                DestroyImmediate(this);
            }
        }
        instance = this;
    }
    void Start()
    {
        GameStart();
    }

    void GameStart()
    {
        planetList = new List<Planet>(FindObjectsOfType<Planet>());
        planetLess = planetList.Count;
    }

    void Update()
    {
        if(planetLess <=0)
        {
            gameStates = GameStates.gameOver;
            winPanel.SetActive(true);
        }
        if(gameStates == GameStates.gameOver )
        {
            Debug.Log("Voy a perder");
            losePanel.SetActive(true);
        }
    }
}

public enum GameStates
{
    gameStart,
    gameIdle,
    gameOver
}
