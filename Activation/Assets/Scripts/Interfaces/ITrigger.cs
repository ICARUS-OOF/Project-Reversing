using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ProjectReversing.Interfaces
{
    public interface ITrigger
    {
        bool isTriggered { get; set; }
        void Trigger();
    }
}