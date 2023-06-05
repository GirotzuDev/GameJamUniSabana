using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : CoreGravity
{
    [SerializeField] float customInfluenceRange;
    [SerializeField] float minCoreForce;
    [SerializeField] float maxCoreForce;

    protected override void Start()
    {
        base.Start();
        // Asignar los valores personalizados a las variables de la clase base
        influenceRange = customInfluenceRange;
        minForce = minCoreForce;
        maxForce = maxCoreForce;
    }

    void Update()
    {
        transform.Rotate(Vector3.forward, 50f * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))//|| other.CompareTag("Asteroid"))
        {
            // Realizar acciones cuando el jugador colisione con el agujero negro

            Rigidbody2D playerRigidbody = other.GetComponent<Rigidbody2D>();

            // 1. Establecer la inercia del jugador en cero
            playerRigidbody.velocity = Vector2.zero;
            playerRigidbody.angularVelocity = 0f;

            // 2. Desactivar el collider del jugador para que no pueda salir del área
            other.GetComponent<Collider2D>().enabled = false;

            // 3. Hacer que el jugador gire rápidamente sobre su eje
            playerRigidbody.AddTorque(5000f, ForceMode2D.Force);

            // 4. Reducir constantemente la escala del jugador hasta 0.1
            StartCoroutine(ScalePlayer(other.transform));

            // 5. Desactivar el game object del jugador cuando la escala llegue a 0.1
            StartCoroutine(DeactivatePlayer(other.gameObject, 1f));
        }
    }

    IEnumerator ScalePlayer(Transform playerTransform)
    {
        Vector3 targetScale = new Vector3(0.1f, 0.1f, 0.1f);
        float scaleSpeed = 0.5f;

        while (playerTransform.localScale.x > 0.1f)
        {
            playerTransform.localScale = Vector3.Lerp(playerTransform.localScale, targetScale, Time.deltaTime * scaleSpeed);
            yield return null;
        }
    }

    IEnumerator DeactivatePlayer(GameObject playerObject, float delay)
    {
        yield return new WaitForSeconds(delay);
        playerObject.SetActive(false);
    }
}
