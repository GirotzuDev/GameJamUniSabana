using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float forwardForce = 5f;

    private Rigidbody2D rb;
    [SerializeField]
    private PlayerState state;
    
    public float maxStamina = 100f; 
    public float staminaConsumptionRate = 10f; // Stamina consumption rate per second
    public float currentStamina;
    public Image staminaSlider;

    public float maxPlantingTime = 100f; 
    public float plantingRate = 10f; // Stamina consumption rate per second    
    public float plantingTime;
    public Image plantingSliter;
    private bool planting = false;

    private Planet actualPlanet;
    [SerializeField]
    private AmebaLife ameba; 

    public Animator playerAnimator;
    public AudioSource jetpackBoost;
    public AudioSource powerUpJetpackBoost;
    public AudioSource playerDeath;
    public AudioSource playerHurt;
    public AudioSource playerLanding;
    public AudioSource playerSeending;
    public AudioSource playerPukeBacteria;  
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        state = PlayerState.notInPropultion;
        staminaSlider = GameObject.Find("Stamina").GetComponent<Image>();
        currentStamina = maxStamina;
        staminaSlider.fillAmount = currentStamina;
        plantingSliter = GameObject.Find("PlantingSliter").GetComponent<Image>();
        plantingTime = 0;
        ameba = gameObject.GetComponent<AmebaLife>();
        playerAnimator = GetComponent<Animator>(); 
    }

    private void Update()
    {
        if (GameManager.Instance.gameStates == GameStates.gameOver || GameManager.Instance.gameStates == GameStates.gameIdle) return;
        if(currentStamina<=0)
        {
            playerDeath.Play();
            GameManager.Instance.gameStates = GameStates.gameOver;
           // GameObject.FindWithTag("GameOverPanel").gameObject.SetActive(true);
        }
        float rotationInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.forward * rotationInput * rotationSpeed * Time.deltaTime*-1);

        
        switch(state)
        {
            case PlayerState.inPropulsion:
                if (Input.GetKeyUp(KeyCode.Space) || currentStamina <= 0)
                {
                    state = PlayerState.notInPropultion;
                    jetpackBoost.Stop();
                }
                StaminaUpdate(-1);

            break;

            case PlayerState.notInPropultion:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    jetpackBoost.Play();
                    state = PlayerState.inPropulsion;
                }
            break;

            case PlayerState.onPlanet:
                playerAnimator.SetTrigger("landing");
                Debug.Log(plantingSliter.fillAmount);
                StaminaUpdate(1);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    StartPlanting(true);
                    playerSeending.Play();
                }
                if (Input.GetKeyUp(KeyCode.E))
                {
                    StartPlanting(false);
                    playerSeending.Stop();
                    powerUpJetpackBoost.Play();
                }
                if(planting)
                {
                    PlantingUpdate(1);
                }
                if(plantingSliter.fillAmount>=1)
                {
                    Debug.Log("Setear gracity");
                    plantingSliter.fillAmount = 0;
                    plantingTime = 0;
                    //actualPlanet.SetGravityMode();
                    GameManager.Instance.planetLess-=1;
                    ameba.state = "planted";
                    Destroy(actualPlanet);
                    state = PlayerState.notInPropultion;
                }
            break;
        }
    }

    private void FixedUpdate()
    {
        switch(state)
        {
            case PlayerState.inPropulsion:
                playerAnimator.SetTrigger("propulsion");
                rb.AddForce(transform.up * forwardForce, ForceMode2D.Force);
            break;

            case PlayerState.notInPropultion:
                playerAnimator.SetTrigger("idle");
            break;
        }
    }
    
    private void StaminaUpdate(int scalar)
    {
        
        currentStamina = currentStamina + (staminaConsumptionRate * Time.deltaTime) * scalar;

        currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        staminaSlider.fillAmount = currentStamina / maxStamina;
    }

    private void PlantingUpdate(int scalar)
    {
        Debug.Log("Planatando ando");
        plantingTime = plantingTime + (plantingRate * Time.deltaTime) * scalar;
        plantingTime = Mathf.Clamp(plantingTime, 0f, maxPlantingTime);
        plantingSliter.fillAmount = plantingTime / maxPlantingTime;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Asteroid")
        {
            playerHurt.Play();
            currentStamina-=col.gameObject.GetComponent<Asteroid>().damage;
            StaminaUpdate(-1);
        }
        if (col.gameObject.tag == "Planet")
        {   
            playerPukeBacteria.Play();
            Debug.Log("Cogere planeta");
            actualPlanet = col.gameObject.GetComponent<Planet>();
            Debug.Log(actualPlanet);
            state = PlayerState.onPlanet;
            ameba.state = "planting";
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Planet")
        {
            state = PlayerState.notInPropultion;
        }
    }

    void StartPlanting(bool isPlanting)
    {
        plantingTime = 0;
        planting = isPlanting;
    }
public enum PlayerState
{
    inPropulsion,
    notInPropultion,
    onPlanet,
    beenAbsorved
}
}