using UnityEngine;
using LD_GameManager;

public class MoverObjetoConCamara : MonoBehaviour
{
    public Animator ballAnimator;
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

    // Ángulo mínimo y máximo de inclinación de la cámara
    public float anguloMinimo = -80f;
    public float anguloMaximo = 80f;

    // Factor de escala para el movimiento vertical
    public float factorEscalaVertical = 0.5f;

    void Update()
    {
        if (GameManager.instance.currentGameState == GameState.InGame)
        {
            MoverObjeto();
            MoverAltura();
        }
    }

    void MoverObjeto(){
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

        // Si se mantiene presionado el botón derecho del ratón y hay un objeto seleccionado
        if (Input.GetMouseButton(1) && objetoSeleccionado != null)
        {
            // Activa la animacion del ball soul
            ballAnimator.SetBool("Holding",true);

            // Actualizar la posición del objeto hacia la posición del puntero
            Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(rayo, out hit, distanciaMaxima))
            {
                // Calcular la dirección de movimiento basada en la inclinación de la cámara
                float inclinacion = Mathf.Clamp(Camera.main.transform.eulerAngles.x, anguloMinimo, anguloMaximo);
                float movimientoVertical = Mathf.Lerp(-1f, 1f, (inclinacion - anguloMinimo) / (anguloMaximo - anguloMinimo));

                // Calcular la posición del objeto basándose en la dirección del jugador y la cámara
                Vector3 movimientoJugador = transform.forward * Input.GetAxis("Vertical");
                Vector3 movimientoCamara = Camera.main.transform.forward * Input.GetAxis("Vertical");
                Vector3 movimientoLateral = transform.right * Input.GetAxis("Horizontal");

                Vector3 movimientoFinal = (movimientoJugador + movimientoCamara + movimientoLateral).normalized;
                movimientoFinal += Vector3.up * movimientoVertical;

                objetoSeleccionado.position = Vector3.Lerp(objetoSeleccionado.position, hit.point + offset + (movimientoFinal * velocidadMovimiento), velocidadMovimiento * Time.deltaTime);
            }
        }else{
            ballAnimator.SetBool("Holding",false);
        }

        // Si se suelta el botón derecho del ratón, liberar el objeto seleccionado y restaurar la gravedad
        if (Input.GetMouseButtonUp(1))
        {
            if (objetoSeleccionado != null)
            {
                Rigidbody rb = objetoSeleccionado.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.useGravity = true;
                }
                else
                {
                    Debug.LogWarning("El objeto seleccionado no tiene un componente Rigidbody.");
                }

                objetoSeleccionado = null;
            }
            else
            {
                Debug.LogWarning("No hay ningún objeto seleccionado.");
            }
        }

    }

    void MoverAltura(){
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
