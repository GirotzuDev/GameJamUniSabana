using UnityEngine;
using UnityEngine.UI;

public class AmebaLife : MonoBehaviour
{
    public float timerDuration = 60f;
    private float currentLife;
    private Image amebaLifeSliter;
    private void Start()
    {
        amebaLifeSliter = GameObject.Find("AmebaLifeSliter").GetComponent<Image>();
        currentLife = timerDuration;
        amebaLifeSliter.fillAmount = 1;
    }

    private void Update()
    {
        if (GameManager.Instance.gameStates == GameStates.gameOver || GameManager.Instance.gameStates == GameStates.gameIdle) return;
        LifeUpdate();
    }
    
    private void LifeUpdate()
    {
        currentLife-= Time.deltaTime;
        currentLife = Mathf.Clamp(currentLife, 0f, timerDuration);
        amebaLifeSliter.fillAmount = currentLife / timerDuration;
        if(currentLife <=0)
        {
            GameManager.Instance.gameStates = GameStates.gameOver;
        }
    }
}