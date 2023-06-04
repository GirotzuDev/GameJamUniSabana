using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public float smoothSpeed = 0.5f;
    public Vector3 offset;
    public float minY;
    public float maxY;

    private void FixedUpdate()
    {
        // Calculate the target position
        float clampedY = Mathf.Clamp(playerTransform.position.y, minY, maxY);
        Vector3 targetPosition = new Vector3(playerTransform.position.x, clampedY, transform.position.z) + offset;

        // Smoothly move the camera towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}