using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemies : MonoBehaviour
{
    public List<GameObject> enemyPrefabs; // Lista de tipos de enemigos
    public bool repeating = true; // Indica si el spawner se repetirá
    public float interval = 1f; // Intervalo entre spawns
    public int spawnCount = 1; // Cuántos enemigos spawnear en cada intervalo

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // Spawnear el número deseado de enemigos
            for (int i = 0; i < spawnCount; i++)
            {
                SpawnEnemy();
            }

            // Esperar el intervalo antes de spawnear de nuevo
            yield return new WaitForSeconds(interval);

            // Si no se repite, salir del bucle
            if (!repeating)
            {
                break;
            }
        }
    }

    void SpawnEnemy()
    {
        // Elegir un enemigo aleatorio de la lista de prefabs
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];

        // Instanciar el enemigo en la posición del spawner
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}

