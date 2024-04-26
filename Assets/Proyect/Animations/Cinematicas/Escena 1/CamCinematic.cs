using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LD_GameManager;

public class CamCinematic : MonoBehaviour
{
    [SerializeField]
    Animator animatorCN;
    [SerializeField]
    GameObject camera2;
    

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

    public void Cn2(){
        this.gameObject.SetActive(false);
        camera2.SetActive(true);
    }
}
