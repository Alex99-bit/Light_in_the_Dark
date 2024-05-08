using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LD_GameManager;

public class MoverObjetoConCamara : MonoBehaviour
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

                // Calcular el offset entre la posición del objeto y el punto de impacto
                offset = objetoSeleccionado.position - hit.point;
            }
        }

        // Si se mantiene presionado el botón derecho del ratón y hay un objeto seleccionado
        if (Input.GetMouseButton(1) && objetoSeleccionado != null)
        {
            // Actualizar la posición del objeto hacia la posición del puntero
            Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(rayo, out hit, distanciaMaxima))
            {
                // Calcular la posición del objeto basándose en la dirección del jugador y la cámara
                Vector3 movimientoJugador = transform.forward * Input.GetAxis("Vertical");
                Vector3 movimientoCamara = Camera.main.transform.forward * Input.GetAxis("Vertical");
                Vector3 movimientoLateral = transform.right * Input.GetAxis("Horizontal");

                Vector3 movimientoFinal = (movimientoJugador + movimientoCamara + movimientoLateral).normalized;

                objetoSeleccionado.position = Vector3.Lerp(objetoSeleccionado.position, hit.point + offset + (movimientoFinal * velocidadMovimiento), velocidadMovimiento * Time.deltaTime);
            }
        }

        // Si se suelta el botón derecho del ratón, liberar el objeto seleccionado
        if (Input.GetMouseButtonUp(1))
        {
            objetoSeleccionado = null;
        }
    }
}

