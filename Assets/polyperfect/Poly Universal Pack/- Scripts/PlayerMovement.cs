using System;
using UnityEngine;

namespace Polyperfect.Universal
{
    public class PlayerMovement : MonoBehaviour
    {
        public CharacterController controller;
        public Transform cameraTransform; // Referencia al transform de la cámara
        public float speed;
        public float gravity = -9.81f;
        public float jumpHeight = 3f;

        public float speedUp;

        public Transform groundCheck;
        public float groundDistance = 0.4f;
        public LayerMask groundMask;


        public Animator animator;
        Vector3 velocity;
        bool isGrounded;
        
        public float maxAngle = 30f;
        public float minAngle = -40f;

        private void Start() {
            animator = GetComponentInChildren<Animator>();
            cameraTransform = GetComponentInChildren<Camera>().GetComponent<Transform>();
            speedUp = 1;
        }


        // Update is called once per frame
        void Update()
        {
            controller = GetComponent<CharacterController>();
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            // Obtener la rotación actual de la cámara
            Vector3 currentRotation = cameraTransform.localEulerAngles;

            // Obtener el ángulo actual en el eje X
            float clampedXAngle = ClampAngle(currentRotation.x, minAngle, maxAngle);

            // Aplicar la rotación restringida
            cameraTransform.localEulerAngles = new Vector3(clampedXAngle, currentRotation.y, currentRotation.z);

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

            if(Input.GetButton("Fire1") && Input.GetButton("Jump")){
                x = 0;
                z = 0; 
            }
            
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
            // Esto sirve para que el salto dependa de la velocidad o carrerilla que lleve
            jumpHeight = speedUp;

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
            if(Input.GetButtonDown("Fire1") && isGrounded){

                animator.SetTrigger("LightShoot");
            }
        }

        void SpeedUp(){
            float speedUpAux = 0.1f;
            if(Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) && !Input.GetButton("Fire1")){
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

        // Método para restringir un ángulo dentro de un rango específico
        float ClampAngle(float angle, float min, float max)
        {
            if (angle > 180f)
                angle -= 360f;

            angle = Mathf.Clamp(angle, min, max);

            if (angle < 0f)
                angle += 360f;

            return angle;
        }
    }
}