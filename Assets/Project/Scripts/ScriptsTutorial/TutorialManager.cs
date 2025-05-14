using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LD_GameManager;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;

    public GameObject panelDragDrop, panelDisparo, panelShield;

    private void Awake() 
    {
        // Asignar esta instancia como la instancia única del GameManager
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }    
    }

    private void Start() {
        ShowShield(false);
        ShowDisparo(false);
        ShowDragDrop(false);
    }

    public void ShowDragDrop(bool show){
        panelDragDrop.SetActive(show);
    }

    public void ShowDisparo(bool show){
        panelDisparo.SetActive(show);
    }

    public void ShowShield(bool show){
        panelShield.SetActive(show);
    }
}
