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
        // Calcular el factor de mezcla basado en la vida actual
        float blendFactor = 1f - (vidaEnemy / vidaMax);

        // Interpolar entre blanco, amarillo, naranja y rojo
        Color colorInterpolado;
        if (blendFactor < 0.5f)
        {
            colorInterpolado = Color.Lerp(Color.white, Color.yellow, blendFactor * 2f);
        }
        else
        {
            colorInterpolado = Color.Lerp(Color.yellow, Color.red, (blendFactor - 0.5f) * 2f);
        }

        // Establecer el color en el material
        sphere.color = colorInterpolado;
        lightBall.color = colorInterpolado;

        /*ballLight.intensity = 0;
        ballLighning.color = Color.black;*/
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
