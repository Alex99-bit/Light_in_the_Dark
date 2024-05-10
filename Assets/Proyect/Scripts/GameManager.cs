using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

namespace LD_GameManager{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance; // Singleton instance
        public GameObject panelPause;
        public GameState currentGameState;

        // Variable para almacenar el nombre de la escena actual
        public string currentScene;


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