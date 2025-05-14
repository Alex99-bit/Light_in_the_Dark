using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LD_GameManager;
using UnityEngine.UI;

public class SkipCinematicTutorial : MonoBehaviour
{
    public GameObject[] objectsToDisable;
    public GameObject playerCinematic;
    public GameObject btnSkip;

    // Update is called once per frame
    void Update()
    {
        if (!playerCinematic.activeSelf)
        {
            btnSkip.SetActive(false);
        }
        else
        {
            btnSkip.SetActive(true);
        }
    }

    public void Skip()
    {
        GameManager.instance.ChangeGameState(GameState.InGame);
        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(false);
        }
        playerCinematic.SetActive(false);
    }
}
