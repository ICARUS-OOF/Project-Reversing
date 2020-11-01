using ProjectReversing.Data;
using ProjectReversing.Handlers;
using System;
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
        public bool isPaused = false;
        public bool canPause = false;
        public GameObject Crosshair;
        public GameObject PauseMenuUI;
        public GameObject playerObject;
        public GameObject BeginningPanel;
        private void Start()
        {
            BeginningPanel.SetActive(true);
            StartCoroutine(DisableBeginning());
        }
        private IEnumerator DisableBeginning()
        {
            yield return new WaitForSeconds(3f);
            canPause = true;
            BeginningPanel.SetActive(false);
        }
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
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Pause();
            }
            PauseMenuUI.SetActive(isPaused);
            playerObject.SetActive(!isPaused);
        }
        public void Pause()
        {
            if (!canPause)
            {
                return;
            }
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
            Resume();
            GameHandler.singleton.OnPlayerDie?.Invoke(this, EventArgs.Empty);
        }
        public void Exit()
        {
            Time.timeScale = 1f;
            SaveSystem.SaveGame(GameHandler.singleton, GameHandler.SaveFile);
            SceneManager.LoadScene("Lobby");
        }
    }
}