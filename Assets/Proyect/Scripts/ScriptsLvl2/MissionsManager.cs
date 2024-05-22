using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LD_GameManager;
using JetBrains.Annotations;

public class MissionsManager : MonoBehaviour
{
    public static MissionsManager instance;

    [SerializeField]
    MissionList currentMission;

    public GameObject panelMision1;

    void Awake() 
    {
        // Asignar esta instancia como la instancia única
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Se desactivan todos los paneles por si las dudas
        panelMision1.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Inicia el nivel con la primera mision
        newMission(MissionList.eliminateAllEnemies);
    }

    // Update is called once per frame
    void Update()
    {
        missionBehavior();
    }

    void missionBehavior(){
        switch(currentMission){
            case MissionList.eliminateAllEnemies:
                eliminateAllEnemiesMission();
            break;
        }
    }

    // Esto sirve muy similar a un start, solo se ejecuta una ves, para los parametros iniciales de cada mision
    public void newMission(MissionList newMission)
    {
        switch(newMission){
            case MissionList.eliminateAllEnemies:
                panelMision1.SetActive(true);
            break;
        }

        currentMission = newMission;
    }

    void eliminateAllEnemiesMission(){
        // aqui va la logica de la mision, y los parametros que compruevan si ya se completo o no

    }

    // Listas de misiones para este nivel
    public enum MissionList{
        // mision 1
        eliminateAllEnemies
    }
}
