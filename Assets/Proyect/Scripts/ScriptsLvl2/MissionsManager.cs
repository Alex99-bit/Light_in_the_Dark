using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LD_GameManager;
using JetBrains.Annotations;
using UnityEngine.UI;

public class MissionsManager : MonoBehaviour
{
    public static MissionsManager instance;

    [SerializeField]
    MissionList currentMission;

    [Space(10)] // Le da un espacio en el inspector

    [Header("Mision 1: Staff")]
    [Space(5)]
    #region "Variables: Eliminate all enemies"
        public GameObject panelMision1;
        public GameObject panelEndDemo;
        public Text misionUnoTxt;
        [SerializeField]
        int numberOfEnemies = 0;
        GameObject[] enemies;
        public float waitTime = 5, currentTime;
    #endregion;

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
        SetOffAllPanels();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Inicia el nivel con la primera mision
        currentMission = MissionList.eliminateAllEnemies;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.currentGameState == GameState.InGame){
            missionBehavior();
        }else{
            SetOffAllPanels();
        }
    }

    void missionBehavior(){
        switch(currentMission){
            case MissionList.eliminateAllEnemies:
                EliminateAllEnemiesMission();
                panelMision1.SetActive(true);
            break;
            
            case MissionList.noCurrentMission:
                // Lo que sucede cuando se completaron todas las misiones del nivel
                SetOffAllPanels();
            break;

            case MissionList.endDemo:
                EndDemo();
            break;
        }
    }

    void SetOffAllPanels()
    {
        panelMision1.SetActive(false);
        panelEndDemo.SetActive(false);
    }

    void EliminateAllEnemiesMission(){
        // aqui va la logica de la mision, y los parametros que compruevan si ya se completo o no
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        numberOfEnemies = enemies.Length;

        misionUnoTxt.text = "Freed souls:   " + numberOfEnemies;

        if(numberOfEnemies <= 0){
            currentTime += Time.deltaTime;
            if(currentTime >= waitTime){
                // mision completa
                Debug.Log("Mision completa muchachos, vamonos!!");
                currentMission = MissionList.endDemo;
            }
        }
    }

    void EndDemo(){
        panelMision1.SetActive(false);
        panelEndDemo.SetActive(true);
    }

    // Listas de misiones para este nivel
    public enum MissionList{
        // mision 1
        eliminateAllEnemies,
        noCurrentMission,
        endDemo
    }
}
