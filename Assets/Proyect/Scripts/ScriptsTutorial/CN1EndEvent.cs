using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LD_GameManager;

public class CN1EndEvent : MonoBehaviour
{
    public GameObject ballCN1, playerCN1;

    public void CN1End(){
        ballCN1.SetActive(false);
        playerCN1.SetActive(false);
        GameManager.instance.ChangeGameState(GameState.InGame);
    }
}
