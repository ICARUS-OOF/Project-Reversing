using ProjectReversing.Handlers;
using UnityEngine;
namespace ProjectReversing.Traits
{
    public class TimeController : MonoBehaviour
    {
        #region Singleton
        public static TimeController singleton;
        private void Awake()
        {
            if (singleton == null)
            {
                singleton = this;
            }
        }
        #endregion
        public float DeltaTime;
        public float TimeScale;
        public bool TimeSlowed = false;
        private void Update()
        {
            if (PlayerUI.singleton.isPaused)
            {
                return;
            }
            DeltaTime = Time.fixedDeltaTime * TimeScale;
            if (Input.GetKeyDown(KeyHandler.ControlTime))
            {
                TimeSlowed = !TimeSlowed;
            }
            if (TimeSlowed)
            {
                TimeScale = Mathf.Lerp(TimeScale, 0f, Time.fixedDeltaTime * 5f);
            } else
            {
                TimeScale = Mathf.Lerp(TimeScale, 1f, Time.fixedDeltaTime * 5f);
            }
        }
    }
}