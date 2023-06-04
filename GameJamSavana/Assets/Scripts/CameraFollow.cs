using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public float smoothSpeed = 0.5f;
    public Vector3 offset;

    private void FixedUpdate()
    {
        // Calculate the target position
        Vector3 targetPosition = new Vector3(playerTransform.position.x, transform.position.y, transform.position.z) + offset;

        // Smoothly move the camera towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}