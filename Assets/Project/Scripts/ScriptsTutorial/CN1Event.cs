using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LD_GameManager;

public class CN1Event : MonoBehaviour
{
    public Animator ballAnimator;
    public GameObject cam1CN1, cam2CN1, cam3CN1, cam4CN1;

    private void Start() {
        GameManager.instance.ChangeGameState(GameState.cinematic);
        cam1CN1.SetActive(true);
        cam2CN1.SetActive(false);
        cam3CN1.SetActive(false);
        cam4CN1.SetActive(false);
    }

    private void Update() {
        if(GameManager.instance.currentGameState == GameState.InGame){
            SetOfAllCams();
        }
    }

    public void CN1EventBallLight(){
        Debug.Log("Si se ejecuta perrooooooooooooo");
        ballAnimator.SetTrigger("Next1");
    } 

    public void Cam2()
    {
        cam2CN1.SetActive(true);
        cam1CN1.SetActive(false);
    }

    public void Cam3(){
        cam3CN1.SetActive(true);
        cam2CN1.SetActive(false);
    }

    public void Cam4(){
        cam4CN1.SetActive(true);
        cam3CN1.SetActive(false);
    }

    public void SetOfAllCams(){
        cam1CN1.SetActive(false);
        cam2CN1.SetActive(false);
        cam3CN1.SetActive(false);
        cam4CN1.SetActive(false);
    }
}
