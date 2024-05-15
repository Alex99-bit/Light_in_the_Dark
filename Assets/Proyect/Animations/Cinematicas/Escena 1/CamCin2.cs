using System.Collections;
using System.Collections.Generic;
using LD_GameManager;
using UnityEngine;

public class CamCin2 : MonoBehaviour
{
    [SerializeField]
    GameObject playerNPC, thisGameObject;

    private void Start() {
        //player.enabled = false;
        //this.gameObject.SetActive(false);
        //thisGameObject = this.GetComponent<GameObject>();
        thisGameObject.SetActive(false);
    }

    public void FinishScene(){
        GameManager.instance.ChangeGameState(GameState.InGame);
        //player.enabled = true;
        playerNPC.SetActive(false);
        thisGameObject.SetActive(false);
    }
}
