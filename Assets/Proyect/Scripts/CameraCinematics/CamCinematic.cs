using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LD_GameManager;

public class CamCinematic : MonoBehaviour
{
    [SerializeField]
    Animator animatorCN;
    

    // Start is called before the first frame update
    void Start()
    {
        animatorCN = this.GetComponent<Animator>();
        GameManager.instance.ChangeGameState(GameState.cinematic);
        Cn1();
    }

    public void Cn1()
    {
        animatorCN.SetTrigger("CN1");
    }

    void FinishCinematic(){
        GameManager.instance.ChangeGameState(GameState.InGame);
        this.gameObject.SetActive(false);
    }
}
