using UnityEngine;
using LD_GameManager;

namespace Polyperfect.Universal
{
    public class MouseLook : MonoBehaviour
    {
        public float mouseSensitivity = 3f;
        public Transform playerBody;
        public float yMaximum = 30f;
        public float yMinimum = -30f;
        float xRotation = 0f;



        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            if (GameManager.instance.currentGameState == GameState.InGame)
            {
                float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
                float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

                // Ajustar la rotación vertical
                xRotation -= mouseY;
                xRotation = Mathf.Clamp(xRotation, yMinimum, yMaximum);

                // Aplicar la rotación vertical a la cámara
                transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

                // Aplicar la rotación horizontal al cuerpo del jugador
                if (playerBody != null)
                {
                    playerBody.Rotate(Vector3.up * mouseX);
                }
            }
        }
    }
}
  