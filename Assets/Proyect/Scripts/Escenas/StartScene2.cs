using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LD_GameManager;

public class StartScene2 : MonoBehaviour
{
    public GameObject cameraHouseDevil;
    public GameObject playerCM1;
    //public SkinnedMeshRenderer player;

    //En lugar de iniciar la escena, pasa a una cinematica
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            //player.enabled = false;
            playerCM1.SetActive(true);
            cameraHouseDevil.SetActive(true);
        }
    }
    
}
