using ProjectReversing.Handlers;
using UnityEngine;
namespace ProjectReversing.Objects
{
    public class CheckPoint : MonoBehaviour
    {
        bool isTriggered = false;
        [SerializeField] Transform CheckPointObject;
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
        private void Update()
        {
            CheckPointObject.Rotate(new Vector3(0f, 4f * Time.fixedDeltaTime, 0f));
        }
    }
}