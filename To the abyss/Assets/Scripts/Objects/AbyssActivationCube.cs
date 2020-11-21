using System.Collections;
using ProjectReversing.Traits;
using ProjectReversing.Interfaces;
using UnityEngine;
namespace ProjectReversing.Objects
{
    public class AbyssActivationCube : ActivationCube
    {
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
            }
            else if (!ignoreTime && !TimeController.singleton.TimeSlowed)
            {
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }
        }
    }
}