using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroideMovement : MonoBehaviour
{
    public float speed = 5.0f; // Velocidad de movimiento
    public float rotationSpeed = 50.0f; // Velocidad de rotaci�n

    
    void Update()
    {
        // Rotar el asteroide sobre el eje Y
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

}

