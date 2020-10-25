using ProjectReversing.Enums;
using ProjectReversing.Interfaces;
using ProjectReversing.Traits;
using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
namespace ProjectReversing.Objects
{
    public class ActivationCube : MonoBehaviour, IHoldable
    {
        public ActivationColor activationColor;
        public float waitOnPickup = 0.2f;
        public float breakForce = 35f;
        public bool pickedUp { get; set; }
        public Light spotLight;
        public PlayerInteraction playerInteractions { get; set; }
        void Start()
        {
            switch (GetComponent<MeshRenderer>().material.name)
            {
                case "ACT_RED (Instance)":
                    activationColor = ActivationColor.Red;
                    break;
                case "ACT_BLUE (Instance)":
                    activationColor = ActivationColor.Blue;
                    break;
                case "ACT_YELLOW (Instance)":
                    activationColor = ActivationColor.Yellow;
                    break;
                case "ACT_GREEN (Instance)":
                    activationColor = ActivationColor.Green;
                    break;
            }
            spotLight.color = GetComponent<MeshRenderer>().material.color;
        }
        public IEnumerator Hold()
        {
            yield return new WaitForSecondsRealtime(waitOnPickup);
            pickedUp = true;
        }
    }
}
