using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace LD_GameManager{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance; // Singleton instance
        public GameObject panelPause;
        public GameState currentGameState;

        // Variable para almacenar el nombre de la escena actual
        public string currentScene;

        #region "Pooling de balas player"
            public GameObject bulletPlayerPrefab;
            public int poolSizePlayer;
            private List<GameObject> bulletPlayerPool;
        #endregion;

        #region "Pooling de balas enemy"
            public GameObject bulletEnemyPrefab;
            public int poolSizeEnemy;
            private List<GameObject> bulletEnemyPool;
        #endregion;


        void Awake()
        {
            // Asignar esta instancia como la instancia única del GameManager
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Start() {
            if(poolSizeEnemy == 0){
                poolSizeEnemy = 50;
            }

            if(poolSizePlayer == 0){
                poolSizePlayer = 15;
            }

            if(currentScene != "Lobby" || currentScene != "Level1"){
                SetPlayerBulletPool(); 
                SetEnemyBulletPool();
            }
        }


        void Update()
        {
            // Logica para la pausa
            if(currentGameState == GameState.InGame){
                if(Input.GetButtonDown("pause")){
                    ChangeGameState(GameState.Pause);
                }
            } else if(currentGameState == GameState.Pause){
                if(Input.GetButtonDown("pause")){
                    ChangeGameState(GameState.InGame);
                }
            }
        }

        public void ChangeGameState(GameState newGameState)
        {
            currentGameState = newGameState;

            // Realizar acciones específicas según el nuevo estado del juego
            switch (currentGameState)
            {
                case GameState.MainMenu:
                    // Código para mostrar el menú principal
                    break;
                case GameState.InGame:
                    // Código para comenzar el juego
                    // Oculta el cursor del ratón al iniciar el juego
                    Cursor.visible = false;
                    // Bloquea el cursor en el centro de la pantalla
                    Cursor.lockState = CursorLockMode.Locked;
                    //freeLookCamera.enabled = true; // Activar la cámara
                    //panelPause.SetActive(false);
                    Time.timeScale = 1;
                    break;
                case GameState.Pause:
                    // Código para pausar el juego
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    //freeLookCamera.enabled = false; // Desactivar la cámara
                    //panelPause.SetActive(true);
                    Time.timeScale = 0;
                    break;
                case GameState.cinematic:
                    // De momento nada
                    break;
                case GameState.GameOver:
                    // Código para mostrar la pantalla de Game Over
                    break;
            }
        }

        // Método para cargar una nueva escena
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        // Método para obtener el nombre de la escena actual
        public string GetCurrentScene()
        {
            return currentScene = SceneManager.GetActiveScene().name;
        }

        public void ExitGame(){
            Application.Quit();
        }

        public GameObject GetPlayerBullet() {
            foreach (GameObject bullet in bulletPlayerPool) {
                if (!bullet.activeInHierarchy) {
                    return bullet;
                }
            }
            GameObject newBullet = Instantiate(bulletPlayerPrefab);
            bulletPlayerPool.Add(newBullet);
            return newBullet;
        }

        public GameObject GetEnemyBullet() {
            foreach (GameObject bullet in bulletEnemyPool) {
                if (!bullet.activeInHierarchy) {
                    return bullet;
                }
            }
            GameObject newBullet = Instantiate(bulletEnemyPrefab);
            bulletEnemyPool.Add(newBullet);
            return newBullet;
        }

        void SetPlayerBulletPool(){
            bulletPlayerPool = new List<GameObject>();
            for (int i = 0; i < poolSizePlayer; i++) {
                GameObject bullet = Instantiate(bulletPlayerPrefab);
                bullet.SetActive(false);
                bulletPlayerPool.Add(bullet);
            }
        }

        void SetEnemyBulletPool(){
            bulletEnemyPool = new List<GameObject>();
            for (int i = 0; i < poolSizeEnemy; i++) {
                GameObject bullet = Instantiate(bulletEnemyPrefab);
                bullet.SetActive(false);
                bulletEnemyPool.Add(bullet);
            }
        }

    }

    public enum GameState
    {
        MainMenu,
        InGame,
        cinematic,
        Pause,
        GameOver
    }
}