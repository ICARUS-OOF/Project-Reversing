using ProjectReversing.Handlers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ProjectReversing.Utils
{
    public class ButtonManagement : MonoBehaviour, IPointerEnterHandler
    {
        public void OnPointerEnter(PointerEventData eventData)
        {
            AudioHandler.PlaySoundEffect("Highlight");
        }
    }
}