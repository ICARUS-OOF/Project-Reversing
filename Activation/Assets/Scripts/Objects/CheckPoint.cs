using ProjectReversing.Handlers;
using UnityEngine;
namespace ProjectReversing.Objects
{
    public class CheckPoint : MonoBehaviour
    {
        bool isTriggered = false;
        private void OnTriggerEnter(Collider col)
        {
            if (isTriggered)
            {
                return;
            }
            if (col.transform.tag == ConstantHandler.PLAYER_TAG)
            {
                GameHandler.LastCheckPointPos = transform.position;
                isTriggered = true;
            }
        }
    }
}