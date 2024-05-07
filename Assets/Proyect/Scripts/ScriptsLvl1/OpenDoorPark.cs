using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorPark : MonoBehaviour
{
    Animator animatorDoor;
    bool isOpen;

    private void Start() {
        animatorDoor = this.GetComponent<Animator>();
        isOpen = false;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player") && !isOpen){
            isOpen = true;
            animatorDoor.SetTrigger("Open");
        }
    }
}
