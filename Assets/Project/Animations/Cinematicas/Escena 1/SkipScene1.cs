using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LD_GameManager;
using UnityEngine.UI;

public class SkipScene1 : MonoBehaviour
{

    public GameObject cinematic;
    public GameObject playerCinematic;
    public GameObject player;
    public GameObject BtnSkip;

    private void Update() {
        if (!playerCinematic.activeSelf)
        {
            BtnSkip.SetActive(false);
        }
        else
        {
            BtnSkip.SetActive(true);
        }
    }

    public void Skip()
    {
        GameManager.instance.ChangeGameState(GameState.InGame);
        cinematic.SetActive(false);
        playerCinematic.SetActive(false);
    }
}
