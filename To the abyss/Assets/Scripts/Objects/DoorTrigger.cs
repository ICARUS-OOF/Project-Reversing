using ProjectReversing.Handlers;
using ProjectReversing.Interfaces;
using ProjectReversing.Traits;
using UnityEngine;
namespace ProjectReversing.Objects
{
    public class DoorTrigger : MonoBehaviour, ITrigger
    {
        [SerializeField] private int triggersCount = 1;
        [SerializeField] private bool timeReliant = true;
        private int _CurrentTriggerCount;
        public bool isTriggered { get; set; }
        private Vector3 origScale;
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
                AudioHandler.PlaySoundEffect("Puzzle Solved");
            }
        }
        public void UnTrigger()
        {
            isTriggered = false;
            _CurrentTriggerCount--;
        }
        private void FixedUpdate()
        {
            if (timeReliant)
            {
                if (isTriggered)
                {
                    transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(transform.localScale.x, 0f, transform.localScale.z), TimeController.singleton.DeltaTime * ConstantHandler.TriggerLerpSpeed);
                }
                else
                {
                    transform.localScale = Vector3.Lerp(transform.localScale, origScale, TimeController.singleton.DeltaTime * ConstantHandler.TriggerLerpSpeed);
                }
            } else
            {
                if (isTriggered)
                {
                    transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(transform.localScale.x, 0f, transform.localScale.z), Time.fixedDeltaTime * ConstantHandler.TriggerLerpSpeed);
                }
                else
                {
                    transform.localScale = Vector3.Lerp(transform.localScale, origScale, Time.fixedDeltaTime * ConstantHandler.TriggerLerpSpeed);
                }
            }
        }
    }
}