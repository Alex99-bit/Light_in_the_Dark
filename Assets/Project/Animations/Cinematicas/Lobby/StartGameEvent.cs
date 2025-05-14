using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameEvent : MonoBehaviour
{
    public void StartEvent_Game(){
        LD_GameManager.GameManager.instance.LoadScene("Level1");
    }
}
