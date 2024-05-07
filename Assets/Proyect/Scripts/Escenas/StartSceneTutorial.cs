using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneTutorial : MonoBehaviour
{
    public void TutorialStart(){
        LD_GameManager.GameManager.instance.LoadScene("LvlTutorial");
    }
}
