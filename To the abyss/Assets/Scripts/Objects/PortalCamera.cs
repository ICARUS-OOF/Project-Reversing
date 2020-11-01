using ProjectReversing.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ProjectReversing.Objects
{
    public class PortalCamera : MonoBehaviour
    {
        private Transform playerCam;
        public Transform portal;
        public Transform otherPortal;

        private void Start()
        {
            playerCam = CameraMovement.singleton.transform;
        }

        private void Update()
        {
            Vector3 playerOffsetFromPortal = playerCam.position - otherPortal.position;
            transform.position = portal.position + playerOffsetFromPortal;

            float angularDifferenceBetweenPortalRotations = Quaternion.Angle(portal.rotation, otherPortal.rotation);

            Quaternion portalRotationalDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortalRotations, Vector3.up);
            Vector3 newCameraDirection = portalRotationalDifference * playerCam.forward;
            transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
        }
    }
}