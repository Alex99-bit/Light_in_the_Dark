using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTipe1 : MonoBehaviour
{
    #region "Light stuff"
    public Light lightBall;
    public Material sphere;
    #endregion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Bullet Light")){
            Destroy(other.gameObject,0.5f);
        }
    }
}
