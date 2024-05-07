using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LD_GameManager;

public class StartScene2 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            // Cargar una nueva escena 
            GameManager.instance.LoadScene("Level2");
        }
    }
}
