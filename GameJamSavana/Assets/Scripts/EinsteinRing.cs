using UnityEngine;

public class EinsteinRing : MonoBehaviour
{
    public Material material;
    public float radius = 10f;
    public float speed = 1f;

    private void Update()
    {
        if (material != null)
        {
            // Update the shader properties
            material.SetFloat("_Radius", radius);
            material.SetFloat("_Speed", speed);
        }
    }
}