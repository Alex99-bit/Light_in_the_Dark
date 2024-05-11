using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        this.GameObject().SetActive(false);
    }
}
