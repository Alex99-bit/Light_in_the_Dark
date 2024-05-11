using System.Collections;
using System.Collections.Generic;
using LD_GameManager;
using UnityEngine;

public class BulletEvent : MonoBehaviour
{
    public Polyperfect.Universal.PlayerMovement player;

    public GameObject bulletPrefab; // Prefab del proyectil
    public Transform firePoint; // Punto de origen del disparo
    public float bulletForce;
    public Transform cameraTransform;

    public Animator camAnimator;

    private void Awake()
    {
        player = GetComponentInParent<Polyperfect.Universal.PlayerMovement>();
    }

    void ShootingLightEvent(){
        // Obtener la dirección de disparo basada en la rotación de la cámara
        Vector3 shootDirection = cameraTransform.forward;

        // Obtener un proyectil del pool en lugar de instanciar uno nuevo
        GameObject bullet = GameManager.instance.GetBullet();

        // Configurar la posición y la rotación del proyectil
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = Quaternion.LookRotation(shootDirection);

        // Activar el proyectil
        bullet.SetActive(true);

        // Obtener el componente Rigidbody del proyectil
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        // Resetear la velocidad del proyectil
        bulletRb.velocity = Vector3.zero;
        bulletRb.angularVelocity = Vector3.zero;

        // Aplicar una fuerza hacia adelante al proyectil
        bulletRb.AddForce(shootDirection * bulletForce, ForceMode.Impulse);

        // En lugar de destruir el proyectil, lo desactivamos después de un tiempo
        //StartCoroutine(DeactivateBullet(bullet, 5f));
    }

    IEnumerator DeactivateBullet(GameObject bullet, float delay) {
        yield return new WaitForSeconds(delay);
    
        // Obtener el componente Rigidbody del proyectil
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        // Solo desactivar la bala si ya no está en movimiento
        if (bulletRb.velocity.magnitude < 0.1f) {
            bullet.SetActive(false);
        }
    }



    void SetCaminarFalse()
    {
        player.SetPuedeCaminarFalse();
    }

    void SetCaminarTrue()
    {
        player.SetPuedeCaminarTrue();
    }

    void SetCamArrived(){
        camAnimator.SetBool("Arrived",true);
    }

    void UnSetCamArrived(){
        camAnimator.SetBool("Arrived",false);
    }
}
