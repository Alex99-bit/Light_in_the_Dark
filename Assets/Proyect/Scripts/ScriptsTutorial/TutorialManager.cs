using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LD_GameManager;

public class TutorialManager : MonoBehaviour
{
    public GameObject panelDragDrop, panelDisparo, panelShield;

    private void Start() {
        panelDisparo.SetActive(false);
        panelDragDrop.SetActive(false);
        panelShield.SetActive(false);
    }

    public void ShowDragDrop(){
        panelDragDrop.SetActive(true);
    }

    public void ShowDisparo(){
        panelDisparo.SetActive(true);
    }

    public void ShowShield(){
        panelShield.SetActive(true);
    }
}
