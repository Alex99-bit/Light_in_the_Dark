using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSpawnEnemies : MonoBehaviour
{
    public GameObject spawnerEnemies;

    // Panel para el tutorial shield
    public GameObject panelTutorialShield;
    bool panelShielActive;

    // Panel para el tutorial shoot
    public GameObject panelTutorialShoot;
    bool panelShootActive;

    bool enemyAlreadySpawn;

    // Tiempo para mostrar los paneles
    public float timeMax = 2, currentTime;

    private void Start() {
        panelShielActive = false;
        panelShootActive = false;

        panelTutorialShield.SetActive(panelShielActive);
        panelTutorialShoot.SetActive(panelShootActive);

        spawnerEnemies.SetActive(false);
        enemyAlreadySpawn = false;

        currentTime = 0;
    }

    private void Update() 
    {
        if (panelShielActive)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= timeMax)
            {
                currentTime = 0;
                SetOffTutorialShield();
                SetActiveTutorialShoot();
            }
        }

        if (panelShootActive)
        {
            currentTime += Time.deltaTime;

            if(currentTime >= timeMax){
                currentTime = 0;
                SetOffTutorialShoot();
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            if(!enemyAlreadySpawn){
                enemyAlreadySpawn = true;
                SetSpawnActive();
                SetActiveTutorialShield();
            }
        }
    }

    void SetSpawnActive(){
        spawnerEnemies.SetActive(true);
    }

    void SetActiveTutorialShoot(){
        panelShootActive = true;
        panelTutorialShoot.SetActive(panelShootActive);
        Time.timeScale = 0.3f;
    }

    void SetActiveTutorialShield(){
        panelShielActive = true;
        panelTutorialShield.SetActive(panelShielActive);
        Time.timeScale = 0.3f;
    }

    void SetOffTutorialShield(){
        panelShielActive = false;
        panelTutorialShield.SetActive(panelShielActive);
        Time.timeScale = 1f;
    }

    void SetOffTutorialShoot(){
        panelShootActive = false;
        panelTutorialShoot.SetActive(panelShootActive);
        Time.timeScale = 1f;
    }
}
