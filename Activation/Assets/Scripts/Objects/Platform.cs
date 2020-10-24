using ProjectReversing.Interfaces;
using ProjectReversing.Traits;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ProjectReversing.Objects
{
    public class Platform : MonoBehaviour, ITrigger
    {
        public bool isTriggered { get; set; }
        public int triggersCount = 1;
        private int _CurrentTriggerCount;
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

        private void Update()
        {
            if (isTriggered)
            {
                if (TimeController.singleton.TimeSlowed)
                {
                    GetComponent<Animator>().enabled = false;
                } else
                {
                    GetComponent<Animator>().enabled = true;
                }
            } else
            {
                GetComponent<Animator>().enabled = false;
            }
        }
    }
}