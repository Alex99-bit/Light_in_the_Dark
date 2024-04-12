using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTipe1 : MonoBehaviour
{
    #region "Light stuff"
    public Light lightBall;
    public Material sphere;
    #endregion;

    #region "Vida"
    public int vidaEnemy, vidaMax;
    #endregion;

    // Start is called before the first frame update
    void Start()
    {
        vidaEnemy = vidaMax;
    }

    // Update is called once per frame
    void Update()
    {
        #region "UI diegetica para la vida"
        // Calculamos el valor normalizado inverso de la vida actual (entre 0 y 1)
        float normalizedInverseLife = 1f - (vidaEnemy / vidaMax);

        // Calculamos el color entre rojo y negro
        Color newColor = Color.Lerp(Color.red, Color.black, normalizedInverseLife);

        // Asignamos el nuevo color a la esfera
        sphere.color = newColor;

        // Calculamos la intensidad entre 100 y 0
        float newIntensity = Mathf.Lerp(100f, 0f, normalizedInverseLife);

        // Asignamos la nueva intensidad a la luz
        lightBall.intensity = newIntensity;
        #endregion;

        if(vidaEnemy <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Bullet Light")){
            Destroy(other.gameObject,0.1f);
            vidaEnemy -= 25;
        }
    }
}
