using ProjectReversing.Handlers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ProjectReversing.Utils
{
    public class ButtonManagement : MonoBehaviour, IPointerEnterHandler
    {
        public void OnPointerEnter(PointerEventData eventData)
        {
            AudioHandler.PlaySoundEffect("Select");
        }
    }
}