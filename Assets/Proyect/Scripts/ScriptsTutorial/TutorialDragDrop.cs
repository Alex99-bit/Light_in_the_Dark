using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDragDrop : MonoBehaviour
{
    // Tiempo que transcurre antes de cerrarse
    public float timeMax = 2, currentTime;
    
    // Panel Drag Drop
    bool panelDragDrop;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
        panelDragDrop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (panelDragDrop)
        {
            currentTime += Time.deltaTime;
            if(currentTime >= timeMax){
                currentTime = 0;
                panelDragDrop = false;
                TutorialManager.instance.ShowDragDrop(panelDragDrop);
            }
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Player")){
            panelDragDrop = true;
            TutorialManager.instance.ShowDragDrop(panelDragDrop);
        }    
    }
}
