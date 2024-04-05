using UnityEngine;

namespace Polyperfect.Universal
{
    public class PlayerMovement : MonoBehaviour
    {
        public CharacterController controller;
        public float speed = 12f;
        public float gravity = -9.81f;
        public float jumpHeight = 3f;

        public Transform groundCheck;
        public float groundDistance = 0.4f;
        public LayerMask groundMask;


        public Animator animator;
        Vector3 velocity;
        bool isGrounded;

        private void Start() {
            animator = GetComponentInChildren<Animator>();
        }


        // Update is called once per frame
        void Update()
        {
            controller = GetComponent<CharacterController>();
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            Walk();
            Jump();
        }

        void Walk()
        {
            if (isGrounded && velocity.y < 0)
            {
                controller.slopeLimit = 45.0f;
                velocity.y = -2f;
            }


            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;
            if (move.magnitude > 0){
                move /= move.magnitude;
                animator.SetBool("Walk",true);
                animator.SetBool("Idle",false);
                animator.SetBool("Jump",false);
            }else{
                animator.SetBool("Walk",false);
                animator.SetBool("Idle",true);
                animator.SetBool("Jump",false);
            }
                

            controller.Move(move * speed * Time.deltaTime);
        }

        void Jump()
        {
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                animator.SetBool("Jump",true);
                animator.SetBool("Walk",false);
                controller.slopeLimit = 100.0f;
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        }
    }
}