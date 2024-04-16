using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTipe1 : MonoBehaviour
{
    #region "Light stuff"
    // Referencia al renderer del objeto
    public Renderer meshRenderer;

    // Colores originales y de da�o
    private Color colorOriginal;
    public Color nuevoColor;

    #endregion;

    #region "Vida"
    public int vidaEnemy, vidaMax;
    #endregion;

    // Duraci�n del cambio de color
    public float duracionCambioColor = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        // Inicializar la vida
        vidaEnemy = vidaMax;

        // Obtener el color original
        colorOriginal = meshRenderer.material.color;
    }

    // Update is called once per frame
    void Update()
    { 
        if (vidaEnemy <= 0)
        {
            // En su lugar ira una animacion de muerte y despues de un tiempo desaparecera el cadaver
            Destroy(this.gameObject);
        }
    }

    // M�todo para cambiar el color del objeto
    public void CambiarColor(Color color)
    {
        // Asignar el nuevo color al material del objeto
        meshRenderer.material.color = color;
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Bullet Light")){
            Destroy(other.gameObject);
            // Aplicar da�o
            vidaEnemy -= 25;

            // Cambiar color al recibir da�o
            CambiarColor(nuevoColor);

            // Iniciar la corrutina para restablecer el color original
            StartCoroutine(RestaurarColorOriginal());
        }
    }

    // Corrutina para restablecer el color original despu�s de un tiempo
    IEnumerator RestaurarColorOriginal()
    {
        // Esperar la duraci�n del cambio de color
        yield return new WaitForSeconds(duracionCambioColor);

        // Restablecer el color original
        CambiarColor(colorOriginal);
    }
}
