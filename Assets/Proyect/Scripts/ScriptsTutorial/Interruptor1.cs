using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interruptor1 : MonoBehaviour
{
    Rigidbody rigidbodyBase;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyBase = GetComponent<Rigidbody>();
        rigidbodyBase.useGravity = false;
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Platform")){
            rigidbodyBase.useGravity = true;
        }
    }
}
