using ProjectReversing.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ProjectReversing.Objects
{
    public class CollisionBox : MonoBehaviour
    {
        [SerializeField] private string colliderTag = "Player";
        [SerializeField] private MonoBehaviour triggerScript;
        private void OnTriggerEnter(Collider col)
        {
            if (col.transform.tag == colliderTag)
            {
                ITrigger _trigger = (ITrigger)triggerScript;
                if (_trigger != null)
                {
                    _trigger.Trigger();
                } else
                {
                    Debug.LogError("Unable to cast trigger script as ITrigger interface");
                }
            }   
        }
        private void OnTriggerExit(Collider col)
        {
            if (col.transform.tag == colliderTag)
            {
                ITrigger _trigger = (ITrigger)triggerScript;
                if (_trigger != null)
                {
                    _trigger.UnTrigger();
                }
                else
                {
                    Debug.LogError("Unable to cast trigger script as ITrigger interface");
                }
            }
        }
    }
}