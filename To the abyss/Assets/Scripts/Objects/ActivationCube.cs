using ProjectReversing.Enums;
using ProjectReversing.Interfaces;
using ProjectReversing.Traits;
using System.Collections;
using UnityEngine;
namespace ProjectReversing.Objects
{
    public class ActivationCube : MonoBehaviour, IHoldable
    {
        public ActivationColor activationColor;
        [SerializeField] private float waitOnPickup = 0.2f;
        [SerializeField] private float breakForce = 35f;

        public bool ignoreTime = false;
        public bool pickedUp { get; set; }
        [SerializeField] private Light spotLight;
        public PlayerInteraction playerInteractions { get; set; }
        private void Update()
        {
            if (!ignoreTime && TimeController.singleton.TimeSlowed)
            {
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                if (PlayerInteraction.singleton.currentlyPickedUpObject == gameObject)
                {
                    pickedUp = false;
                    PlayerInteraction.singleton.currentlyPickedUpObject = null;
                }
            } else if (!ignoreTime && !TimeController.singleton.TimeSlowed)
            {
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }
        }
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
