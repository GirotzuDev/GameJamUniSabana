using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMovement : MonoBehaviour
{
    void Update()
    {
        // Rotate the object constantly
        transform.Rotate(Vector3.forward, 10f * Time.deltaTime);
    }
}
