using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LD_GameManager;

public class TheEnd : MonoBehaviour
{
    // Se ejecuta una vez que se termina el demo
    public void EndDemo()
    {
        GameManager.instance.ChangeGameState(GameState.MainMenu);
        GameManager.instance.LoadScene("Lobby");
    }
}
