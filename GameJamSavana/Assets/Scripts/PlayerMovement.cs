using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float forwardForce = 5f;

    private Rigidbody2D rb;

    private PlayerState state;
    
    public float maxStamina = 100f; 
    public float staminaConsumptionRate = 10f; // Stamina consumption rate per second
    public float currentStamina;
    public Image staminaSlider;

    public float maxPlantingTime = 100f; 
    public float plantingRate = 10f; // Stamina consumption rate per second    
    public float plantingTime;
    public Image plantingSliter;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        state = PlayerState.notInPropultion;
        staminaSlider = GameObject.Find("Stamina").GetComponent<Image>();
        currentStamina = maxStamina;
        staminaSlider.fillAmount = currentStamina;
        plantingSliter = GameObject.Find("PlantingSliter").GetComponent<Image>();
        plantingTime = 0;

    }

    private void Update()
    {
        float rotationInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.forward * rotationInput * rotationSpeed * Time.deltaTime*-1);
        
        switch(state)
        {
            case PlayerState.inPropulsion:
                if (Input.GetKeyUp(KeyCode.Space) || currentStamina <= 0)
                {
                    state = PlayerState.notInPropultion;
                }
                StaminaUpdate(-1);

            break;

            case PlayerState.notInPropultion:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    state = PlayerState.inPropulsion;
                }
            break;

            case PlayerState.planting:
                if (Input.GetKeyUp(KeyCode.E))
                {
                    state = PlayerState.notInPropultion;
                }
                PlantingUpdate(1);
            break;
        }
    }

    private void FixedUpdate()
    {
        switch(state)
        {
            case PlayerState.inPropulsion:
                rb.AddForce(transform.up * forwardForce, ForceMode2D.Force);
            break;

            case PlayerState.notInPropultion:

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

        plantingTime = plantingTime + (plantingRate * Time.deltaTime) * scalar;
        plantingTime = Mathf.Clamp(plantingTime, 0f, maxPlantingTime);
        plantingSliter.fillAmount = plantingTime / maxPlantingTime;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Asteroid")
        {
            currentStamina-=col.gameObject.GetComponent<Asteroid>().damage;
            StaminaUpdate(-1);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartPlanting();
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Planet")
        {
            state = PlayerState.notInPropultion;
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Planet")
        {
            StaminaUpdate(1);
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartPlanting();
            }
        }
    }

    void StartPlanting()
    {
        plantingTime = 0;
        state = PlayerState.planting;
    }
public enum PlayerState
{
    inPropulsion,
    notInPropultion,
    planting,
    beenAbsorved
}
}