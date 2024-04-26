using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCinematic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.ChangeGameState(GameState.cinematic);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FinishCinematic(){
        GameManager.instance.ChangeGameState(GameState.InGame);
    }
}
