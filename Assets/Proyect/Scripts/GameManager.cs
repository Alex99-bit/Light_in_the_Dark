using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace LD_GameManager{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance; // Singleton instance

        #region "Objetos para los paneles e interfaces"
        public GameObject panelPause, panelSettings;
        #endregion;

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

        [SerializeField]
        bool cinematicOn;


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
            PauseLogic();
        }

        public void ChangeGameState(GameState newGameState)
        {
            currentGameState = newGameState;

            // Realizar acciones específicas según el nuevo estado del juego
            switch (currentGameState)
            {
                case GameState.MainMenu:
                    // Código para mostrar el menú principal+
                    panelPause.SetActive(false);
                    cinematicOn = false;
                    break;
                case GameState.InGame:
                    // Código para comenzar el juego
                    // Oculta el cursor del ratón al iniciar el juego
                    Cursor.visible = false;
                    cinematicOn = false;
                    // Bloquea el cursor en el centro de la pantalla
                    Cursor.lockState = CursorLockMode.Locked;
                    panelPause.SetActive(false);
                    Time.timeScale = 1;
                    break;
                case GameState.Pause:
                    // Código para pausar el juego
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    panelPause.SetActive(true);
                    panelSettings.SetActive(false);
                    Time.timeScale = 0;
                    break;
                case GameState.Settings:
                    panelPause.SetActive(false);
                    panelSettings.SetActive(true);
                    break;
                case GameState.cinematicPause:
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    panelPause.SetActive(true);
                    panelSettings.SetActive(false);
                    Time.timeScale = 0;
                    break;
                case GameState.cinematic:
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    cinematicOn = true;
                    panelPause.SetActive(false);
                    Time.timeScale = 1;
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

        public void ContinueGame(){
            if(cinematicOn){
                ChangeGameState(GameState.cinematic);
            }else {
                ChangeGameState(GameState.InGame);
            }
        }

        public void GoToMainMenu(){
            LoadScene("Lobby");
        }

        public void Settings(){
            ChangeGameState(GameState.Settings);
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

        void PauseLogic(){
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

            // Pausa para cinematicas
            if(currentGameState == GameState.cinematic){
                if(Input.GetButtonDown("pause")){
                    ChangeGameState(GameState.cinematicPause);
                }
            }else if(currentGameState == GameState.cinematicPause){
                if(Input.GetButtonDown("pause")){
                    ChangeGameState(GameState.cinematic);
                }
            }

            // Back de settings a pause
            if(currentGameState == GameState.Settings){
                if(Input.GetButtonDown("pause") && cinematicOn){
                    ChangeGameState(GameState.cinematicPause);
                }
                else if(Input.GetButtonDown("pause") && !cinematicOn){
                    ChangeGameState(GameState.Pause);
                }
            }
        }

    }

    public enum GameState
    {
        MainMenu,
        InGame,
        cinematic,
        cinematicPause,
        Pause,
        Settings,
        GameOver
    }
}