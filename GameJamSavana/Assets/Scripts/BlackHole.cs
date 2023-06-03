using UnityEngine;

public class BlackHole : CoreGravity
{
    [SerializeField] float customInfluenceRange;
    [SerializeField] float customIntensity;

    protected override void Start()
    {
        base.Start();
        // Asignar los valores personalizados a las variables de la clase base
        influenceRange = customInfluenceRange;
        intensity = customIntensity;

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Realizar acciones cuando el jugador colisione con el agujero negro
            Debug.Log("muertiao");
        }
    }
}
