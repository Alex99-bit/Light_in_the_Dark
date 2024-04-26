using System.Collections;
using System.Collections.Generic;
using LD_GameManager;
using UnityEngine;

public class CamCin2 : MonoBehaviour
{
    [SerializeField]
    GameObject player, playerNPC;

    private void Start() {
        player.SetActive(false);
        this.gameObject.SetActive(false);
    }

    public void FinishScene(){
        LD_GameManager.GameManager.instance.ChangeGameState(GameState.InGame);
        player.SetActive(true);
        playerNPC.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
