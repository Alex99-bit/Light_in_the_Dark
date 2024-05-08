using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LD_GameManager;

public class AlturaObjetos : MonoBehaviour
{
    // Capa que contiene los objetos que se pueden mover
    public LayerMask capaObjetosMovibles;

    // Distancia máxima del raycast
    public float distanciaMaxima = 10f;

    // Velocidad de movimiento de los objetos
    public float velocidadMovimiento = 5f;

    // Objeto actualmente seleccionado para mover
    private Transform objetoSeleccionado;

    // Offset para mantener la distancia entre el objeto y el puntero
    private Vector3 offset;

    // Referencia al controlador del jugador
    private CharacterController controladorJugador;

    // Factor de escala para el movimiento vertical
    public float factorEscalaVertical = 0.5f;

    void Start()
    {
        // Obtener referencia al controlador del jugador
        controladorJugador = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Si se presiona el botón derecho del ratón
        if (Input.GetMouseButtonDown(1))
        {
            // Lanzar un raycast desde la posición de la cámara, ignorando al jugador
            Ray rayo = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(rayo, out hit, distanciaMaxima, capaObjetosMovibles))
            {
                // Guardar la referencia del objeto seleccionado
                objetoSeleccionado = hit.transform;

                // Desactivar la gravedad del objeto seleccionado
                objetoSeleccionado.GetComponent<Rigidbody>().useGravity = false;

                // Calcular el offset entre la posición del objeto y el punto de impacto
                offset = objetoSeleccionado.position - hit.point;
            }
        }

        // Si se mueve la rueda del mouse
        float scrollWheelInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheelInput != 0f && objetoSeleccionado != null)
        {
            // Aplicar el desplazamiento vertical al objeto seleccionado
            Vector3 desplazamientoVertical = Vector3.up * scrollWheelInput * factorEscalaVertical;
            objetoSeleccionado.position += desplazamientoVertical;
        }

        // Si se suelta el botón derecho del ratón, liberar el objeto seleccionado y restaurar la gravedad
        if (Input.GetMouseButtonUp(1))
        {
            objetoSeleccionado.GetComponent<Rigidbody>().useGravity = true;
            objetoSeleccionado = null;
        }
    }
}
