using ProjectReversing.Interfaces;
using ProjectReversing.Traits;
using System.Collections;
using UnityEngine;
namespace ProjectReversing.Movement
{
    public class MovingPlatform : MonoBehaviour, ITrigger
    {
        public bool TimeReliant = true;
        public float dirChangeDelay = 1f;
        public float velDuration = 10f;
        public Vector3 platformVelocity;
        private Rigidbody rb;
        bool isSwapped = false;

        public bool isTriggered { get; set; }

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            StartCoroutine(InitMovingPlatform());
        }
        IEnumerator InitMovingPlatform()
        {
            yield return new WaitForSeconds(0.00001f);
            for ( ; ; )
            {
                if (TimeController.singleton.TimeSlowed && TimeReliant)
                {
                    continue;
                }
                if (isSwapped)
                {
                    rb.velocity = -platformVelocity * TimeController.singleton.DeltaTime;
                } else
                {
                    rb.velocity = platformVelocity * TimeController.singleton.DeltaTime;
                }
                yield return new WaitForSeconds(velDuration);
                rb.velocity = Vector3.zero;
                isSwapped = !isSwapped;
                yield return new WaitForSeconds(dirChangeDelay);
                yield return null;
            }
        }
        public void Trigger()
        {
            TimeController.singleton.transform.parent = transform;
        }
        public void UnTrigger()
        {
            TimeController.singleton.transform.parent = null;
        }
    }
}