using ProjectReversing.Handlers;
using ProjectReversing.Interfaces;
using ProjectReversing.Traits;
using UnityEngine;
namespace ProjectReversing.Objects
{
    public class WindPad : MonoBehaviour, ITrigger
    {
        public ParticleSystem windParticles;
        public Vector3 windVel;
        public bool isTriggered { get; set; }

        public bool autoTrigger = false;

        private int _CurrentTriggerCount = 0;
        public int NeededTriggerCount = 1;

        private void Start()
        {
            isTriggered = autoTrigger;
        }

        public void Trigger()
        {
            _CurrentTriggerCount++;
            if (_CurrentTriggerCount >= NeededTriggerCount)
            {
                isTriggered = true;
            } 
        }

        public void UnTrigger()
        {
            isTriggered = false;
            _CurrentTriggerCount--;
        }

        private void OnTriggerEnter(Collider col)
        {
            if (!isTriggered || TimeController.singleton.TimeSlowed)
            {
                return;
            }
            if (col.transform.tag == ConstantHandler.PLAYER_TAG)
            {
                Rigidbody player_rb = col.GetComponent<Rigidbody>();
                player_rb.AddForce(windVel * 180);
            }
        }

        private void Update()
        {
            if (isTriggered && !TimeController.singleton.TimeSlowed)
            {
                if (!windParticles.isPlaying)
                {
                    windParticles.Play();
                }
            } else
            {
                windParticles.Pause();
            }
        }
    }
}