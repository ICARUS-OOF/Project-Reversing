using ProjectReversing.Traits;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace ProjectReversing.Movement
{
    public class CameraMovement : MonoBehaviour
    {
        #region Singleton
        public static CameraMovement singleton;
        private void Awake()
        {
            if (singleton == null)
            {
                singleton = this;
            }
        }
        #endregion
        public Transform playerHead;
        public PostProcessVolume PPV;
        Vignette vignetteLayer;
        ChromaticAberration chromaticAberrationLayer;
        private void Start()
        {
            PPV.profile.TryGetSettings(out vignetteLayer);
            PPV.profile.TryGetSettings(out chromaticAberrationLayer);
        }
        void Update()
        {
            if (PlayerUI.singleton != null)
            {
                if (PlayerUI.singleton.isPaused)
                {
                    return;
                }
            }
            transform.position = playerHead.position;
            if (TimeController.singleton != null)
            {
                if (TimeController.singleton.TimeSlowed)
                {
                    vignetteLayer.enabled.value = true;
                    vignetteLayer.intensity.value = Mathf.Lerp(vignetteLayer.intensity.value, 0.4f, Time.fixedDeltaTime * 5f);

                    chromaticAberrationLayer.enabled.value = true;
                    chromaticAberrationLayer.intensity.value = Mathf.Lerp(chromaticAberrationLayer.intensity.value, 0.8f, Time.fixedDeltaTime * 5f);
                } else
                {
                    vignetteLayer.enabled.value = true;
                    vignetteLayer.intensity.value = Mathf.Lerp(vignetteLayer.intensity.value, 0.22f, Time.fixedDeltaTime * 5f);

                    chromaticAberrationLayer.enabled.value = true;
                    chromaticAberrationLayer.intensity.value = Mathf.Lerp(chromaticAberrationLayer.intensity.value, 0.2f, Time.fixedDeltaTime * 5f);
                }
            }
        }
    }
}