using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameManager : MonoBehaviour
{
    #region "Start Game Staff"
        public Animator cameraStartGame;
    #endregion;

    //Metodo para iniciar la trancision para iniciar el juego
    public void StartGame(){
        cameraStartGame.SetTrigger("StartGame");
    }
}
