using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LD_GameManager;

public class CamCinematic : MonoBehaviour
{
    [SerializeField]
    GameObject camera2;
    

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.ChangeGameState(GameState.cinematic);
    }

    public void Cn2(){
        this.gameObject.SetActive(false);
        camera2.SetActive(true);
    }
}
