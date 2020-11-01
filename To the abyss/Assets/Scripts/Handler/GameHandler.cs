using ProjectReversing.Data;
using ProjectReversing.Movement;
using ProjectReversing.Traits;
using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
namespace ProjectReversing.Handlers
{
    public class GameHandler : MonoBehaviour
    {
        #region Singleton
        public static GameHandler singleton;
        private void Awake()
        {
            if (singleton == null)
            {
                singleton = this;
            }
        }
        #endregion
        #region Properties
        public static float sensitivity = 1f;
        public static float volume = 1f;
        public static float GFX = 1f;
        public static string SaveFile;
        public static Vector3 LastCheckPointPos = new Vector3(2.75f, 0f, 1.490116e-08f);
        #endregion
        #region Events
        public EventHandler OnPlayerDie;
        public EventHandler<Vector3> OnCheckPointReached;
        #endregion
        #region Normal Methods
        private void Start()
        {
            GameObject[] objs = GameObject.FindGameObjectsWithTag("GameHandler");
            if (objs.Length > 1)
            {
                Destroy(this.gameObject);
                return;
            } else
            {
                DontDestroyOnLoad(this.gameObject);
            }
            OnPlayerDie += onPlayerDie;
            OnCheckPointReached += onCheckPointReached;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (SceneManager.GetActiveScene().name == "Main")
            {
                PlayerMovement.singleton.GetComponent<Rigidbody>().isKinematic = true;
                PlayerMovement.singleton.GetComponent<PlayerMovement>().enabled = false;
                PlayerMovement.singleton.transform.position = LastCheckPointPos;
                PlayerMovement.singleton.GetComponent<Rigidbody>().isKinematic = false;
                PlayerMovement.singleton.GetComponent<PlayerMovement>().enabled = true;
            } else if (SceneManager.GetActiveScene().name == "Lobby")
            {
                LastCheckPointPos = new Vector3(2.75f, 0f, 1.490116e-08f);
            }
        }
        private void Update()
        {
            Options options = Options.singleton;
            if (Options.singleton != null)
            {
                sensitivity = options.GetSensitivity();
                volume = options.GetVolume();
                GFX = options.GetGFX();
            }
            PostProcessVolume[] PPVs = GameObject.FindObjectsOfType<PostProcessVolume>();
            for (int i = 0; i < PPVs.Length; i++)
            {
                PPVs[i].weight = GFX;
            }
        }
        void onPlayerDie(object sender, EventArgs e)
        {
            if (LastCheckPointPos != Vector3.zero)
            { 
                PlayerMovement.singleton.GetComponent<Rigidbody>().isKinematic = true;
                PlayerMovement.singleton.GetComponent<PlayerMovement>().enabled = false;
                PlayerMovement.singleton.transform.position = LastCheckPointPos;
                PlayerMovement.singleton.GetComponent<Rigidbody>().isKinematic = false;
                PlayerMovement.singleton.GetComponent<PlayerMovement>().enabled = true;
            } else
            {
                PlayerMovement.singleton.GetComponent<Rigidbody>().isKinematic = true;
                PlayerMovement.singleton.GetComponent<PlayerMovement>().enabled = false;
                PlayerMovement.singleton.transform.position = Vector3.zero;
                PlayerMovement.singleton.GetComponent<Rigidbody>().isKinematic = false;
                PlayerMovement.singleton.GetComponent<PlayerMovement>().enabled = true;
            }
        }
        void onCheckPointReached(object sender, Vector3 _pos)
        {
            LastCheckPointPos = _pos;
        }
        private void OnApplicationQuit()
        {
            SaveSystem.SaveGame(this, SaveFile);
        }
        #endregion
        #region Static Methods
        public static void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        public static void FreeCursor()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        #endregion
    }
}