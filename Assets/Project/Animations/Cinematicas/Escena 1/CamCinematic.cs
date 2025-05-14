using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LD_GameManager;

public class CamCinematic : MonoBehaviour
{
    [SerializeField]
    GameObject camera2,cameraThis;
    

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.ChangeGameState(GameState.cinematic);
        //cameraThis = this.GetComponent<GameObject>();
    }

    public void Cn2(){
        camera2.SetActive(true);
        cameraThis.SetActive(false);
    }
}
