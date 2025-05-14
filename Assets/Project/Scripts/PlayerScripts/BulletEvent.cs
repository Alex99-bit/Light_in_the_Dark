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

    private Dictionary<GameObject, Coroutine> deactivateRoutines = new Dictionary<GameObject, Coroutine>();

    private void Awake()
    {
        player = GetComponentInParent<Polyperfect.Universal.PlayerMovement>();
    }

    void ShootingLightEvent(){
        // Obtener la dirección de disparo basada en la rotación de la cámara
        Vector3 shootDirection = cameraTransform.forward;

        // Obtener un proyectil del pool en lugar de instanciar uno nuevo
        GameObject bullet = GameManager.instance.GetPlayerBullet();

        // Configurar la posición y la rotación del proyectil
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = Quaternion.LookRotation(shootDirection);
        
        // Activar el proyectil
        bullet.SetActive(true);

        // Si la bala ya tiene una corrutina de desactivación, cancelarla
        if (deactivateRoutines.ContainsKey(bullet)) {
            StopCoroutine(deactivateRoutines[bullet]);
            deactivateRoutines.Remove(bullet);
        }

        // Obtener el componente Rigidbody del proyectil
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        // Resetear la velocidad del proyectil
        bulletRb.velocity = Vector3.zero;
        bulletRb.angularVelocity = Vector3.zero;

        // Aplicar una fuerza hacia adelante al proyectil
        bulletRb.AddForce(shootDirection * bulletForce, ForceMode.Impulse);

        // En lugar de destruir el proyectil, lo desactivamos después de un tiempo
        Coroutine deactivateRoutine = StartCoroutine(DeactivateBullet(bullet, 5f));
        deactivateRoutines.Add(bullet, deactivateRoutine);
    }


    IEnumerator DeactivateBullet(GameObject bullet, float delay) {
        yield return new WaitForSeconds(delay);
        
        // Solo desactivar la bala si todavía está activa
        if (bullet.activeInHierarchy) {
            bullet.SetActive(false);
        }

        // Eliminar la referencia a la corrutina
        if (deactivateRoutines.ContainsKey(bullet)) {
            deactivateRoutines.Remove(bullet);
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
