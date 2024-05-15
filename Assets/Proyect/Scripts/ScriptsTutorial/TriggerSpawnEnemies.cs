using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSpawnEnemies : MonoBehaviour
{
    public GameObject spawnerEnemies;

    bool enemyAlreadySpawn;

    private void Start() {
        spawnerEnemies.SetActive(false);
        enemyAlreadySpawn = false;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            if(enemyAlreadySpawn){
                enemyAlreadySpawn = false;
                SetSpawnActive();
            }
        }
    }

    void SetSpawnActive(){
        spawnerEnemies.SetActive(true);
    }
}
