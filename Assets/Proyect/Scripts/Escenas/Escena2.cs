using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LD_GameManager;

public class Escena2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Obtener el nombre de la escena actual
        string nombreEscenaActual = GameManager.instance.GetCurrentScene();
        Debug.Log("Est�s en la escena: " + nombreEscenaActual);
    }
}
