using ProjectReversing.Enums;
using ProjectReversing.Handlers;
using ProjectReversing.Interfaces;
using System.Collections.Generic;
using UnityEngine;
namespace ProjectReversing.Objects
{
    public class PortableActivationPad : MonoBehaviour
    {
        private ActivationColor activationColor;
        [SerializeReference]
        public List<MonoBehaviour> TriggersToActivate = new List<MonoBehaviour>();
        public List<MeshRenderer> RenderersToLerp = new List<MeshRenderer>();

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
        }
        private void OnCollisionEnter(Collision collision)
        {
            Collider collider = collision.collider;
            ActivationCube activationCube = collider.GetComponent<ActivationCube>();
            if (activationCube != null && activationCube.activationColor == activationColor)
            {
                AudioHandler.PlaySoundEffect("Door");
                for (int i = 0; i < TriggersToActivate.Count; i++)
                {
                    ITrigger _trigger = TriggersToActivate[i] as ITrigger;
                    _trigger.Trigger();
                }
            }
        }
        private void OnCollisionExit(Collision collision)
        {
            Collider collider = collision.collider;
            ActivationCube activationCube = collider.GetComponent<ActivationCube>();
            if (activationCube != null && activationCube.activationColor == activationColor)
            {
                Debug.Log("Untriggered");
                AudioHandler.PlaySoundEffect("Door");
                for (int i = 0; i < TriggersToActivate.Count; i++)
                {
                    ITrigger _trigger = TriggersToActivate[i] as ITrigger;
                    _trigger.UnTrigger();
                }
            }
        }
    }
}
