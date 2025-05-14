using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    // Cuando choca la bala con cualquier cosa, se destrulle y deja una estela de particulas
    private void OnCollisionEnter(Collision other) {
        this.GameObject().SetActive(false);
    }
}
