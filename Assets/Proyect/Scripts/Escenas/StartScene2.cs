using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LD_GameManager;

public class StartScene2 : MonoBehaviour
{
    public GameObject cameraHouseDevil;
    public GameObject player, playerCM1;

    //En lugar de iniciar la escena, pasa a una cinematica
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            player.SetActive(false);
            playerCM1.SetActive(true);
            cameraHouseDevil.SetActive(true);
        }
    }
    
}
