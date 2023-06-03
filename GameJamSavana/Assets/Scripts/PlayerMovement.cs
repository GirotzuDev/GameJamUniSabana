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

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        state = PlayerState.notInPropultion;
        staminaSlider = GameObject.Find("Stamina").GetComponent<Image>();
        staminaSlider.fillAmount = 1f;
    }

    private void Update()
    {
        float rotationInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.forward * rotationInput * rotationSpeed * Time.deltaTime*-1);
        
        switch(state)
        {
            case PlayerState.inPropulsion:
                if (Input.GetKeyUp(KeyCode.Space))
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
        Debug.Log(currentStamina);

        currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        staminaSlider.fillAmount = currentStamina / maxStamina;
    }

public enum PlayerState
{
    inPropulsion,
    notInPropultion
}
}