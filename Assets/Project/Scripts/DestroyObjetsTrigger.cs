using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjetsTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        Destroy(other.gameObject,0.1f);
    }
}
