using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenFencePark : MonoBehaviour
{
    public GameObject fencePark1, fencePark2, fencePark1Open, fencePark2Open;
    public Animator fenceManor1, fenceManor2;

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            fencePark1.SetActive(false);
            fencePark2.SetActive(false);
            fencePark1Open.SetActive(true);
            fencePark2Open.SetActive(true);

            fenceManor1.SetTrigger("Close");
            fenceManor2.SetTrigger("Close");

            this.gameObject.SetActive(false);
        }
    }
}
