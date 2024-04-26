using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCinematic : MonoBehaviour
{
    Animator animatorCN;

    // Start is called before the first frame update
    void Start()
    {
        animatorCN = GetComponent<Animator>();
        GameManager.instance.ChangeGameState(GameState.cinematic);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Cn1()
    {
        
    }

    void FinishCinematic(){
        GameManager.instance.ChangeGameState(GameState.InGame);
    }
}
