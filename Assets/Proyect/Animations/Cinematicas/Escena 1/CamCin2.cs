using System.Collections;
using System.Collections.Generic;
using LD_GameManager;
using UnityEngine;

public class CamCin2 : MonoBehaviour
{
    public void FinishScene(){
        LD_GameManager.GameManager.instance.ChangeGameState(GameState.InGame);
        this.gameObject.SetActive(false);
    }
}
