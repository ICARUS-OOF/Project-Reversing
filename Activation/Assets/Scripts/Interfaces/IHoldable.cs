using ProjectReversing.Traits;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ProjectReversing.Interfaces
{
    public interface IHoldable
    {
        bool pickedUp { get; set; }
        PlayerInteraction playerInteractions { get; set; }
        IEnumerator Hold();
    }
}