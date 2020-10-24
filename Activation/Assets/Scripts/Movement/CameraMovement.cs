using ProjectReversing.Handlers;
using ProjectReversing.Traits;
using UnityEngine;
namespace ProjectReversing.Movement
{
    public class CameraMovement : MonoBehaviour
    {
        public Transform playerHead;
        void Update()
        {
            if (PlayerUI.singleton.isPaused)
            {
                return;
            }
            transform.position = playerHead.position;
            /*
            if (PlayerUI.singleton.isPaused)
            {
                return;
            }

            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.fixedDeltaTime * GameHandler.sensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.fixedDeltaTime * GameHandler.sensitivity;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -55f, 55f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            playerBody.Rotate(Vector3.up * mouseX);
            */
        }
    }
}