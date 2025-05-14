using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LD_GameManager;

public class Escena1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Cargar una nueva escena llamada "NuevaEscena"
        //GameManager.instance.LoadScene("Level1");

        // Obtener el nombre de la escena actual
        string nombreEscenaActual = GameManager.instance.GetCurrentScene();
        Debug.Log("Estás en la escena: " + nombreEscenaActual);
    }
}
