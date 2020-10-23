using ProjectReversing.Handlers;
using ProjectReversing.Interfaces;
using UnityEngine;
namespace ProjectReversing.Objects
{
    public class DoorTrigger : MonoBehaviour, ITrigger
    {
        public int triggersCount = 1;
        private int _CurrentTriggerCount;
        public bool isTriggered { get; set; }
        public void Trigger()
        {
            _CurrentTriggerCount++;
            if (_CurrentTriggerCount >= triggersCount)
            {
                isTriggered = true;
            }
        }
        private void FixedUpdate()
        {
            if (isTriggered)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(transform.localScale.x, 0f, transform.localScale.z), Time.fixedDeltaTime * ConstantHandler.TriggerLerpSpeed);
            }
        }
    }
}