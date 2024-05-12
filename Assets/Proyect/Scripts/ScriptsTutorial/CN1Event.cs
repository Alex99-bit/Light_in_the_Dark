using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LD_GameManager;

public class CN1Event : MonoBehaviour
{
    public Animator ballAnimator;

    private void Start() {
        GameManager.instance.ChangeGameState(GameState.cinematic);
    }

    public void IniciarAnimacion2BallLight(){
        //Debug.Log("Si se ejecuta perrooooooooooooo");
        ballAnimator.SetTrigger("Next1");
    } 
}
