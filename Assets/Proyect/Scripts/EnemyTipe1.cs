using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTipe1 : MonoBehaviour
{
    #region "Light stuff"
    // Referencia al renderer del objeto
    public Renderer meshRenderer;

    // Colores originales y de daño
    private Color colorOriginal;
    public Color nuevoColor;

    #endregion;

    #region "Vida"
    public int vidaEnemy, vidaMax;
    #endregion;

    // Duración del cambio de color
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

    // Método para cambiar el color del objeto
    public void CambiarColor(Color color)
    {
        // Asignar el nuevo color al material del objeto
        meshRenderer.material.color = color;
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Bullet Light")){
            Destroy(other.gameObject);
            // Aplicar daño
            vidaEnemy -= 25;

            // Cambiar color al recibir daño
            CambiarColor(nuevoColor);

            // Iniciar la corrutina para restablecer el color original
            StartCoroutine(RestaurarColorOriginal());
        }
    }

    // Corrutina para restablecer el color original después de un tiempo
    IEnumerator RestaurarColorOriginal()
    {
        // Esperar la duración del cambio de color
        yield return new WaitForSeconds(duracionCambioColor);

        // Restablecer el color original
        CambiarColor(colorOriginal);
    }
}
