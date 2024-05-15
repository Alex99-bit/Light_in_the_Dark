using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidersSwitchingBallLight : MonoBehaviour
{
    [SerializeField]
    MeshCollider mesh;
    [SerializeField]
    SphereCollider sphere;
    [SerializeField]
    Polyperfect.Universal.PlayerMovement playerScript;

    // Update is called once per frame
    void Update()
    {
        if(playerScript.shieldActive){
            mesh.enabled = true;
            sphere.enabled = false;
        }else{
            mesh.enabled = false;
            sphere.enabled = true;
        }
    }
}
