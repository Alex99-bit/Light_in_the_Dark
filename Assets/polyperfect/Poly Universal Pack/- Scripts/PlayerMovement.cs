using UnityEngine;

namespace Polyperfect.Universal
{
    public class PlayerMovement : MonoBehaviour
    {
        public CharacterController controller;
        public Transform cameraTransform; // Referencia al transform de la cámara
        public GameObject bulletPrefab; // Prefab del proyectil
        public Transform firePoint; // Punto de origen del disparo
        public float fireRate,bulletForce; // Tasa de disparo en segundos
        public float speed = 12f;
        public float gravity = -9.81f;
        public float jumpHeight = 3f;

        public float speedUp;

        public Transform groundCheck;
        public float groundDistance = 0.4f;
        public LayerMask groundMask;


        public Animator animator;
        Vector3 velocity;
        bool isGrounded;

        private void Start() {
            animator = GetComponentInChildren<Animator>();
            speedUp = 1;
        }


        // Update is called once per frame
        void Update()
        {
            controller = GetComponent<CharacterController>();
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            Walk();
            Jump();
            ShootingLight();
        }

        private void FixedUpdate() {
            SpeedUp();
        }

        void Walk()
        {
            if (isGrounded && velocity.y < 0)
            {
                controller.slopeLimit = 45.0f;
                velocity.y = -2f;
                animator.ResetTrigger("Salto");
                animator.SetBool("IsGround",true);
            }


            float x = Input.GetAxis("Horizontal") * speedUp;
            float z = Input.GetAxis("Vertical") * speedUp;

            Vector3 move = transform.right * x + transform.forward * z;
            if (move.magnitude > 0){
                move /= move.magnitude;
            }
                
            animator.SetFloat("XSpeed",x);
            animator.SetFloat("YSpeed",z);

            controller.Move(move * speed * Time.deltaTime);
        }

        void Jump()
        {
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                animator.SetTrigger("Salto");
                animator.SetBool("IsGround",false);
                controller.slopeLimit = 100.0f;
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        }

        void ShootingLight()
        {
            if(Input.GetButtonDown("Fire1")){
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

        void SpeedUp(){
            float speedUpAux = 0.1f;
            if(Input.GetKey(KeyCode.LeftShift)){
                if(speedUp < 2){
                    speedUp += speedUpAux;
                    speed += (speedUpAux * 4f);
                }else{
                    speedUp = 2;
                }
            }else{
                if(speedUp > 1){
                   speedUp -= speedUpAux; 
                   speed -= (speedUpAux * 4f);
                }else{
                    speedUp = 1;
                }
            }
        }
    }
}