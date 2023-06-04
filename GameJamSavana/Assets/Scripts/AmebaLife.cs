using UnityEngine;
using UnityEngine.UI;

public class AmebaLife : MonoBehaviour
{
    public float timerDuration = 60f;
    private float currentLife;
    private Image amebaLifeSliter;
    public string state;

    private void Start()
    {
        amebaLifeSliter = GameObject.Find("AmebaLifeSliter").GetComponent<Image>();
        currentLife = timerDuration;
        amebaLifeSliter.fillAmount = 1;
        state = "dying";
    }

    private void Update()
    {
        if (GameManager.Instance.gameStates == GameStates.gameOver || GameManager.Instance.gameStates == GameStates.gameIdle) return;
        switch(state)
        {
            case "dying":
                LifeUpdate();
            break;
            case "planted":
                Debug.Log("Hola recuperemos");
                currentLife = timerDuration;
                amebaLifeSliter.fillAmount = 1;
                state = "dying";
            break;
        }
    }
    
    private void LifeUpdate()
    {
        currentLife-= Time.deltaTime;
        currentLife = Mathf.Clamp(currentLife, 0f, timerDuration);
        amebaLifeSliter.fillAmount = currentLife / timerDuration;
        if(currentLife <=0)
        {
            GameObject.FindWithTag("GameOverPanel").gameObject.SetActive(true);
            GameManager.Instance.gameStates = GameStates.gameOver;
        }
    }

}