using UnityEngine;
using Cinemachine;

namespace LD_GameManager{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance; // Singleton instance
        public GameObject panelPause;
        public GameState currentGameState;
        public CinemachineFreeLook freeLookCamera;


        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject); // Preservar el objeto GameManager entre escenas
        }

        void Start()
        {
            // Establecer el estado inicial del juego
            //ChangeGameState(GameState.InGame);
        }

        void Update()
        {
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