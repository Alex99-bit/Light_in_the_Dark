using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using LD_GameManager;

public class PlayerMecha : MonoBehaviour
{
    public float moveSpeed, multiplySpeed;  // Velocidad de movimiento del jugador
    public float jumpForce; // Fuerza del salto
    public float rotateSpeed = 500f;  // Velocidad de rotación del jugador
    float startSpeed;
    float horizontalInput, verticalInput;

    // Cosas para disparar la luz
    public GameObject bulletPrefab; // Prefab del proyectil
    public Transform firePoint; // Punto de origen del disparo
    public float fireRate,bulletForce; // Tasa de disparo en segundos
    float nextFireTime = 0; // Tiempo para el próximo disparo
    

    private Rigidbody rb;

    public Animator animator; // Referencia al componente Animator
    public Transform cameraTransform; // Referencia al transform de la cámara
    public CinemachineFreeLook cameraMachine;
    public GameObject panelAim;

    enum FollowCam
    {
        AimOn,
        AimOff
    }

    FollowCam camState;

    void Start()
    {
        //cameraMachine = GameObject.Find("FreeLook Camera").GetComponent<CinemachineFreeLook>();

        camState = FollowCam.AimOn;

        if (moveSpeed == 0)
        {
            moveSpeed = 5f;
        }

        if(fireRate == 0){
            fireRate = 0.5f;
        }

        if(bulletForce == 0){
            bulletForce = 25.5f;
        }

        if (multiplySpeed == 0)
        {
            multiplySpeed = 2.5f * moveSpeed;
        }

        if (jumpForce == 0)
        {
            jumpForce = 3f;
        }

        rb = GetComponent<Rigidbody>();

        // Obtener el componente Animator del GameObject actual si no se ha asignado desde el editor
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        // Obtener la cámara principal si no se ha asignado desde el editor
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }

        startSpeed = moveSpeed;

        cameraMachine.m_Lens.FieldOfView = 50f;
    }

    void Update()
    {
        if (GameManager.instance.currentGameState == GameState.InGame)
        {
            // Obtener la entrada del eje horizontal (teclas A y D o flechas izquierda y derecha)
            horizontalInput = Input.GetAxis("Horizontal");

            // Obtener la entrada del eje vertical (teclas W y S o flechas arriba y abajo)
            verticalInput = Input.GetAxis("Vertical");

            // Obtener la dirección hacia donde está mirando la cámara
            Vector3 cameraForward = Vector3.Scale(cameraTransform.forward, new Vector3(1, 0, 1)).normalized;
            Vector3 moveDirection = cameraForward * verticalInput + cameraTransform.right * horizontalInput;

            // Normalizar solo los componentes X y Z del vector de dirección para evitar el aumento de velocidad en diagonal
            moveDirection = moveDirection.normalized;

            // Calcular el desplazamiento basado en la dirección de la cámara
            Vector3 movement = moveDirection * moveSpeed * Time.deltaTime;

            // Aplicar el movimiento al jugador
            rb.MovePosition(transform.position + movement);

            if (camState == FollowCam.AimOn)
            {
                RotatePlayer(cameraForward);
            }

            if (moveDirection.magnitude > 0)
            {
                RotatePlayer(moveDirection);
            }

            // Si hay entrada de movimiento, activar la animación de "walk"
            if (moveDirection.magnitude > 0)
            {
                print("Walk");
                animator.ResetTrigger("Idle");
                animator.ResetTrigger("Run");
                animator.ResetTrigger("Jump");
                animator.SetTrigger("Walk");

                if (Input.GetButton("run"))
                {
                    moveSpeed = multiplySpeed;
                    animator.ResetTrigger("Walk");
                    animator.ResetTrigger("Jump");
                    animator.SetTrigger("Run");
                }
                else
                {
                    moveSpeed = startSpeed;
                }
            }
            else
            {
                print("NO Walk");
                animator.ResetTrigger("Walk");
                animator.ResetTrigger("Run");
                animator.ResetTrigger("Jump");
                animator.SetTrigger("Idle");
            }

            // Si se presiona la tecla de espacio, llamar al método de salto
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            // Se apunta y logica de apuntado
            if (Input.GetMouseButton(1))
            {
                camState = FollowCam.AimOn;
                panelAim.SetActive(true);

                if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime)
                {
                    Shoot();
                    nextFireTime = Time.time + fireRate; // Actualizar el tiempo para el próximo disparo
                }
            }
            else
            {
                camState = FollowCam.AimOff;
                panelAim.SetActive(false);
            }
        }
    }


    private void FixedUpdate() {
        if (GameManager.instance.currentGameState == GameState.InGame)
        {
            SetAimCam();
        }    
    }

    // Método para saltar 
    void Jump()
    {
        // Activar la animación de salto
        animator.ResetTrigger("Idle");
        animator.ResetTrigger("Run");
        animator.ResetTrigger("Walk");
        animator.SetTrigger("Jump");

        // Verificar si el jugador está en el suelo
        if (IsGrounded())
        {
            // Aplicar una fuerza de salto al Rigidbody
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void SetAimCam()
    {
        if (camState == FollowCam.AimOn)
        {
            //cameraMachine.LookAt = followAim.transform;
            cameraMachine.m_Lens.FieldOfView = 25f;
        }
        else if (camState == FollowCam.AimOff)
        {
            //cameraMachine.LookAt = followNormal.transform;
            cameraMachine.m_Lens.FieldOfView = 50f;
        }
    }

    // Método para verificar si el jugador está en el suelo
    bool IsGrounded()
    {
        // Longitud del rayo
        float rayLength = 0.2f; // Cambia este valor según el margen de holgura que desees

        // Lanzar un rayo hacia abajo desde el centro del jugador
        RaycastHit hit;
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, rayLength);

        return isGrounded;
    }

    void Shoot()
    {
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


    void RotatePlayer(Vector3 cameraForward)
    {
        // Crear una rotación basada en la dirección hacia donde mira la cámara
        Quaternion targetRotation = Quaternion.LookRotation(cameraForward, Vector3.up);

        // Calcular la rotación actual del jugador
        Quaternion currentRotation = rb.rotation;

        // Interpolar suavemente entre la rotación actual y la rotación deseada
        float t = Mathf.Clamp01(rotateSpeed * Time.deltaTime); // Limitar el valor de interpolación entre 0 y 1
        Quaternion newRotation = Quaternion.Slerp(currentRotation, targetRotation, t);

        // Aplicar la rotación al Rigidbody del jugador
        rb.MoveRotation(newRotation);
    }


}