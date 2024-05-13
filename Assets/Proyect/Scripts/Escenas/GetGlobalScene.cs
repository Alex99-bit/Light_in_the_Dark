using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetGlobalScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LD_GameManager.GameManager.instance.GetCurrentScene();   
    }
}
