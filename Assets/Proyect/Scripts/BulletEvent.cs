using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEvent : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab del proyectil
    public Transform firePoint; // Punto de origen del disparo
    public float fireRate,bulletForce; // Tasa de disparo en segundos
    public Transform cameraTransform;

    void ShootingLightEvent(){
        // Obtener la dirección de disparo basada en la rotación de la cámara
        Vector3 shootDirection = cameraTransform.forward;

        // Instanciar el proyectil en el punto de origen del disparo
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(shootDirection));

        // Obtener el componente Rigidbody del proyectil
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        // Aplicar una fuerza hacia adelante al proyectil
        bulletRb.AddForce(shootDirection * bulletForce, ForceMode.Impulse);

        // Destruir el proyectil después de un tiempo (por ejemplo, 3 segundos)
        Destroy(bullet, 3f);
    }
}
