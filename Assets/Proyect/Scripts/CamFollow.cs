using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform target;  // El transform del jugador
    public Vector3 offset;    // La distancia desde el jugador a la cámara

    public float smoothSpeed = 0.125f;  // Velocidad de suavizado

    void LateUpdate()
    {
        if (target == null)
            return;

        // Calcular la posición deseada de la cámara
        Vector3 desiredPosition = target.position + offset;

        // Calcular el movimiento suavizado
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Establecer la posición de la cámara
        transform.position = smoothedPosition;

        // Hacer que la cámara mire hacia el jugador
        transform.LookAt(target);
    }
}
