using ProjectReversing.Interfaces;
using ProjectReversing.Traits;
using UnityEngine;
namespace ProjectReversing.Objects
{
    public class Platform : MonoBehaviour, ITrigger
    {
        public bool ignoreTime = false;
        public bool TriggerOnAwake = false;
        public bool isTriggered { get; set; }
        public int triggersCount = 1;
        private int _CurrentTriggerCount;

        private void Start()
        {
            if (TriggerOnAwake)
            {
                isTriggered = true;
            }
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
            _CurrentTriggerCount--;
        }

        private void Update()
        {
            if (isTriggered)
            {
                if (ignoreTime)
                {
                    GetComponent<Animator>().enabled = true;
                } else
                {
                    if (TimeController.singleton.TimeSlowed)
                    {
                        GetComponent<Animator>().enabled = false;
                    }
                    else
                    {
                        GetComponent<Animator>().enabled = true;
                    }
                }
            } else
            {
                GetComponent<Animator>().enabled = false;
            }
        }
    }
}