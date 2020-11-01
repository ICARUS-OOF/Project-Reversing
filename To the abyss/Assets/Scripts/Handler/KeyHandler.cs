using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ProjectReversing.Handlers
{
    public class KeyHandler : MonoBehaviour
    {
        public static KeyCode Interact = KeyCode.E;
        public static KeyCode Sprint = KeyCode.LeftShift;
        public static KeyCode Jump = KeyCode.Space;
        public static KeyCode Crouch = KeyCode.LeftControl;
        public static KeyCode PickUp = KeyCode.F;
        public static KeyCode ControlTime = KeyCode.Q;
        public static bool isMoving()
        {
            return
            Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.D);
        }
    }
}