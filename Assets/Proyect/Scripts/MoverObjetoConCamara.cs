using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LD_GameManager;
public class MoverObjetoConCamara : MonoBehaviour
{
    // Velocidad de movimiento
    public float velocidadMovimiento = 5f;

    // Referencia a la cámara
    private Camera camara;

    void Start()
    {
        // Obtener la cámara principal
        camara = Camera.main;
    }

    void Update()
    {
        if(GameManager.instance.currentGameState == GameState.InGame){
            // Verificar si se ha presionado el botón de mover (por ejemplo, el botón izquierdo del mouse)
            if (Input.GetMouseButton(0))
            {
                // Rayo desde la cámara hacia el punto en el espacio donde se encuentra el objeto bajo el cursor
                Ray rayo = camara.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                
                // Verificar si el rayo impacta con un objeto
                if (Physics.Raycast(rayo, out hit))
                {
                    // Mover el objeto hacia la posición del cursor
                    transform.position = Vector3.MoveTowards(transform.position, hit.point, velocidadMovimiento * Time.deltaTime);
                }
            }
        }
    }
}

