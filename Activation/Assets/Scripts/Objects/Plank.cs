using ProjectReversing.Traits;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ProjectReversing.Objects
{
    public class Plank : MonoBehaviour
    {
        public Vector3 rotationEuler;
        private void Update()
        {
            if (PlayerUI.singleton.isPaused)
            {
                return;
            }
            transform.Rotate(rotationEuler * TimeController.singleton.DeltaTime);
        }
    }
}