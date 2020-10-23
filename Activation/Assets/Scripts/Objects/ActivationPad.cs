using ProjectReversing.Enums;
using ProjectReversing.Handlers;
using ProjectReversing.Interfaces;
using System.Collections.Generic;
using UnityEngine;
namespace ProjectReversing.Objects
{
    public class ActivationPad : MonoBehaviour
    {
        private GameObject _ActCube;
        public Transform ActCubePoint;
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
                _ActCube = activationCube.gameObject;
                Destroy(activationCube.GetComponent<Rigidbody>());
                Destroy(activationCube);
                for (int i = 0; i < TriggersToActivate.Count; i++)
                {
                    ITrigger _trigger = TriggersToActivate[i] as ITrigger;
                    _trigger.Trigger();
                }
            }
        }
        private void FixedUpdate()
        {
            if (_ActCube != null)
            {
                _ActCube.transform.position = Vector3.Lerp(_ActCube.transform.position, ActCubePoint.position, Time.fixedDeltaTime * ConstantHandler.PadLerpSpeed);
                _ActCube.transform.rotation = Quaternion.Lerp(_ActCube.transform.rotation, ActCubePoint.rotation, Time.fixedDeltaTime * ConstantHandler.PadLerpSpeed);
                for (int i = 0; i < RenderersToLerp.Count; i++)
                {
                    RenderersToLerp[i].material.color = Color.Lerp(RenderersToLerp[i].material.color, GetComponent<MeshRenderer>().material.color, Time.fixedDeltaTime * ConstantHandler.PadLerpSpeed);
                }
            }
        }
    }
}