using ProjectReversing.Data;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

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
        public static Vector3 LastCheckPointPos = Vector3.zero;
        #endregion
        #region Normal Methods
        private void Start()
        {
            GameObject[] objs = GameObject.FindGameObjectsWithTag("GameHandler");
            if (objs.Length > 1)
            {
                Destroy(this.gameObject);
            } else
            {
                DontDestroyOnLoad(this.gameObject);
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