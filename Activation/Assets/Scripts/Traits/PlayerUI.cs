using ProjectReversing.Handlers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ProjectReversing.Traits
{
    public class PlayerUI : MonoBehaviour
    {
        #region Singleton
        public static PlayerUI singleton;
        private void Awake()
        {
            if (singleton == null)
            {
                singleton = this;
            }
        }
        #endregion
        public bool isPaused = false;
        public GameObject Crosshair;
        private void Update()
        {
            if (isPaused)
            {
                GameHandler.FreeCursor();
                Crosshair.SetActive(false);
            } else
            {
                GameHandler.LockCursor();
                Crosshair.SetActive(true);
            }
        }
        public void Pause()
        {
            isPaused = true;
        }
        public void Resume()
        {
            isPaused = false;
        }
    }
}