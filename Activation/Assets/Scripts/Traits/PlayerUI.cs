using ProjectReversing.Handlers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        public bool isLobby = true;
        public bool isPaused = false;
        public GameObject Crosshair;
        public GameObject PauseMenuUI;
        public GameObject playerObject;
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
            if (isLobby)
            {

            } else
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Pause();
                }
                PauseMenuUI.SetActive(isPaused);
            }
            playerObject.SetActive(!isPaused);
        }
        public void Pause()
        {
            isPaused = true;
            Time.timeScale = 0f;
        }
        public void Resume()
        {
            isPaused = false;
            Time.timeScale = 1f;
        }
        public void Respawn()
        {

        }
        public void Exit()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Lobby");
        }
    }
}