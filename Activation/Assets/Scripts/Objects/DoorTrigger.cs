using ProjectReversing.Handlers;
using ProjectReversing.Interfaces;
using ProjectReversing.Traits;
using UnityEngine;
namespace ProjectReversing.Objects
{
    public class DoorTrigger : MonoBehaviour, ITrigger
    {
        public int triggersCount = 1;
        private int _CurrentTriggerCount;
        public bool isTriggered { get; set; }
        Vector3 origScale;
        void Start()
        {
            origScale = transform.localScale;
        }
        public void Trigger()
        {
            _CurrentTriggerCount++;
            if (_CurrentTriggerCount >= triggersCount)
            {
                isTriggered = true;
            }
        }
        public void UnTrigger()
        {
            isTriggered = false;
        }
        private void FixedUpdate()
        {
            if (isTriggered)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(transform.localScale.x, 0f, transform.localScale.z), TimeController.singleton.DeltaTime * ConstantHandler.TriggerLerpSpeed);
            } else
            {
                transform.localScale = Vector3.Lerp(transform.localScale, origScale, TimeController.singleton.DeltaTime * ConstantHandler.TriggerLerpSpeed);
            }
        }
    }
}