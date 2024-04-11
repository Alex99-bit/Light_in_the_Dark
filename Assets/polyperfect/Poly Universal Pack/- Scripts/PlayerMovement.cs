using System;
using UnityEngine;

namespace Polyperfect.Universal
{
    public class PlayerMovement : MonoBehaviour
    {
        #region "Vida"
            public float vidaMax = 100;
            public float vida;
        #endregion;

        #region "Light"
            public Light ballLight;
            public Material ballLighning;
            public GameObject ballSoul;
            public Animator lightAnimator;
            public bool shieldActive;
        #endregion;

        #region "Camera Animation"
            public Animator cameraAnimator;
        #endregion;

        bool ballSoulActive;
        public CharacterController controller;
        public Transform cameraTransform; // Referencia al transform de la cámara
        public float speed;
        public float gravity = -9.81f;
        public float jumpHeight = 3f;

        float rotAux;

        public float speedUp;

        public Transform groundCheck;
        public float groundDistance = 0.4f;
        public LayerMask groundMask;

        Transform thisGameObject;


        public Animator animator;
        Vector3 velocity;
        bool isGrounded;
        
        public float maxAngle = 30f;
        public float minAngle = -40f;

        // Variable para mantener el estado anterior de la rotación de la cámara
        private float previousCameraRotationY;

        private void Start() {
            cameraTransform = GetComponentInChildren<Camera>().GetComponent<Transform>();
            thisGameObject = this.GetComponent<Transform>();
            speedUp = 1;
            rotAux = 0;

            vida = vidaMax;
            shieldActive = false;

            // Cpsas para la pelorira de vida
            ballSoulActive = true;
            ballSoul.SetActive(ballSoulActive);
            ballLighning.color = Color.white;
            ballLight.color = Color.white;

            // Inicializar previousCameraRotationY con la rotación inicial de la cámara
            previousCameraRotationY = thisGameObject.localEulerAngles.y;
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

            #region "UI diegetica para la vida"
                // Calcular el factor de mezcla basado en la vida actual
                float blendFactor = 1f - (vida / vidaMax);

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
                ballLighning.color = colorInterpolado;
                ballLight.color = colorInterpolado;
            #endregion;

            Walk();
            Jump();
            ShootingLight();
            ActiveShield();
        }

        private void FixedUpdate() {
            SpeedUp();
        }

        void Walk()
        {
            Vector3 currentTransform = thisGameObject.localEulerAngles;

            if (isGrounded && velocity.y < 0)
            {
                controller.slopeLimit = 45.0f;
                velocity.y = -2f;
                animator.ResetTrigger("Salto");
                animator.SetBool("IsGround",true);
            }

            if(!isGrounded){
                animator.SetBool("IsGround",false);
                //cameraAnimator.SetTrigger("Jump");
            }else{
                //cameraAnimator.SetTrigger("Arrived");
            }

            float x = Input.GetAxis("Horizontal") * speedUp;
            float z = Input.GetAxis("Vertical") * speedUp;

            if(Input.GetButton("Fire1") && Input.GetButton("Jump")){
                x = 0;
                z = 0; 
            }

            // Calcular el cambio en la rotación de la cámara desde el frame anterior
            float rotationChange = currentTransform.y - previousCameraRotationY;

            //Debug.Log("rotacion: "+RotationAuxiliar(rotationChange));


            if(x == 0){
                // Determinar si la cámara está rotando hacia la derecha o hacia la izquierda
                animator.SetFloat("XSpeed",RotationAuxiliar(rotationChange));
                cameraAnimator.SetFloat("SpeedX",RotationAuxiliar(rotationChange)*0.5f);
            }else{
                animator.SetFloat("XSpeed",x);
                cameraAnimator.SetFloat("SpeedX",x);
            }

            animator.SetFloat("YSpeed",z);

            
            cameraAnimator.SetFloat("SpeedY",z);

            // Actualizar la rotación anterior para el próximo frame
            previousCameraRotationY = currentTransform.y;
            
            Vector3 move = transform.right * x + transform.forward * z;
            if (move.magnitude > 0){
                move /= move.magnitude;
            }

            controller.Move(move * speed * Time.deltaTime);
        }

        void Jump()
        {
            // Esto sirve para que el salto dependa de la velocidad o carrerilla que lleve
            jumpHeight = speedUp;

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                animator.SetTrigger("Salto");
                controller.slopeLimit = 100.0f;
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        }

        void ShootingLight()
        {
            if(Input.GetButtonDown("Fire1") && isGrounded && ballSoulActive && !shieldActive){

                animator.SetTrigger("LightShoot");
                vida -= 15;
            }
        }

        void ActiveShield(){
            if(Input.GetKeyDown(KeyCode.Q) && isGrounded && ballSoulActive){
                shieldActive = true;
                lightAnimator.SetBool("Shield",true);
            }

            if(Input.GetKeyUp(KeyCode.Q) && isGrounded && ballSoulActive && shieldActive){
                shieldActive = false;
                lightAnimator.SetBool("Shield",false);
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

        void GameOver(){
            if(vida <= 0){
                animator.SetBool("IsDead",true);
            }
        }

        void GameReset(){
            vidaMax = 100;
            vida = vidaMax;
            animator.SetBool("IsDead",false);

            // Regresar el transform a un punto de spawn
            // o en su defecto destruir al player 
        }

        float RotationAuxiliar(float rotation)
        {
            float valor = 1f;
            float maxValor = 2f;

            if (rotation > 0)
            {
                rotAux += (valor * (Time.deltaTime * maxValor));
                if (rotAux >= maxValor)
                {
                    rotAux = maxValor;
                }
            }
            else if (rotation < 0)
            {
                rotAux -= (valor * (Time.deltaTime * maxValor));
                if (rotAux <= -maxValor)
                {
                    rotAux = -maxValor;
                }
            }
            else // Si rotation es igual a cero
            {
                if (rotAux >= -0.1)
                {
                    rotAux -= (valor * (Time.deltaTime * maxValor * 1.5f));
                    if (rotAux <= 0)
                    {
                        rotAux = 0;
                    }
                }
                else if (rotAux <= -0.1)
                {
                    rotAux += (valor * (Time.deltaTime * maxValor * 1.5f));
                    if (rotAux >= 0)
                    {
                        rotAux = 0;
                    }
                }
            }

            return rotAux;
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