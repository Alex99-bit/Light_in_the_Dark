using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LD_GameManager;

public class CamCinematic : MonoBehaviour
{
    Animator animatorCN;

    // Start is called before the first frame update
    void Start()
    {
        animatorCN = GetComponent<Animator>();
        GameManager.instance.ChangeGameState(GameState.cinematic);
    }

    public void Cn1()
    {
        animatorCN.SetTrigger("CN1");
    }

    void FinishCinematic(){
        GameManager.instance.ChangeGameState(GameState.InGame);
    }
}
