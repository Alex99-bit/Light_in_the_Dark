using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneTutorial : MonoBehaviour
{
    private void Start() {
        LD_GameManager.GameManager.instance.ChangeGameState(LD_GameManager.GameState.cinematic);
    }

    public void TutorialStart(){
        LD_GameManager.GameManager.instance.LoadScene("LvlTutorial");
    }
}
