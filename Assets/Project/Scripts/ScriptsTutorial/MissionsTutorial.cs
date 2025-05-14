using System.Collections;
using System.Collections.Generic;
using LD_GameManager;
using UnityEngine;

public class MissionsTutorial : MonoBehaviour
{
    public float waitTime = 5, currentTime;

    [SerializeField]
    int numberOfEnemies = 0;
    GameObject[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        // Buscar todos los objetos con el tag "Enemy" y contar cuántos hay
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        numberOfEnemies = enemies.Length;

        currentTime = 0;

        // Imprimir el número de enemigos en la consola para verificar
        Debug.Log("Número de enemigos en la escena: " + numberOfEnemies);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.currentGameState == GameState.InGame){
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            numberOfEnemies = enemies.Length;

            if(numberOfEnemies <= 0){
                currentTime += Time.deltaTime;
                if(currentTime >= waitTime){
                    GameManager.instance.LoadScene("Level2");
                }
            }
        }
    }
}
