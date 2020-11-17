using ProjectReversing.Handlers;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
namespace ProjectReversing.Traits
{
    public class ZaWarudo : MonoBehaviour
    {
        #region Singleton
        public static ZaWarudo singleton;
        private void Awake()
        {
            if (singleton == null)
            {
                singleton = this;
            }
        }
        #endregion

        private PostProcessVolume PPV;
        private Animator anim;

        private float shakeIntesity = 0;

        [SerializeField, Range(-100f, 100f)] private float lensDistortionIntensity = 0;
        [SerializeField, Range(0f, 1f)] private float chromaticAberrationIntensity = .4f;
        [SerializeField, Range(-180, 180)] private float saturation = 0;

        [SerializeField] private Transform cam;

        private LensDistortion lensDistortion;
        private ChromaticAberration chromaticAberration;
        private ColorGrading colorGrading;

        private void Start()
        {
            anim = GetComponent<Animator>();
            PPV = GetComponent<PostProcessVolume>();
            PPV.profile.TryGetSettings<LensDistortion>(out lensDistortion);
            PPV.profile.TryGetSettings<ChromaticAberration>(out chromaticAberration);
            PPV.profile.TryGetSettings<ColorGrading>(out colorGrading);
        }

        private void Update()
        {
            //Lens Distortion
            lensDistortion.enabled.value = true;
            lensDistortion.intensity.value = lensDistortionIntensity;
            
            //Chromatic Aberration
            chromaticAberration.enabled.value = true;
            chromaticAberration.intensity.value = chromaticAberrationIntensity;

            //Color Grading
            colorGrading.enabled.value = true;
            colorGrading.saturation.value = saturation;
        }
        public void MegaStopTime()
        {
            TimeController.singleton.TimeSlowed = true;
            anim.SetTrigger("Za Warudo");
            AudioHandler.PlaySoundEffect("Za Warudo");
            ShakeCamera(.1f, 2f);
        }

        public void ShakeCamera(float intensity, float duration)
        {
            shakeIntesity = intensity;
            InvokeRepeating(nameof(StartCameraShake), 0, .01f);
            Invoke(nameof(StopCameraShake), duration);
        }

        private void StartCameraShake()
        {
            if (shakeIntesity > 0)
            {
                Vector3 camPos = cam.position;

                float offsetX = Random.value * shakeIntesity * 2f - shakeIntesity;
                float offsetY = Random.value * shakeIntesity * 2f - shakeIntesity;

                camPos.x += offsetX;
                camPos.y += offsetY;

                cam.position = camPos;
            }
        }

        private void StopCameraShake()
        {
            CancelInvoke(nameof(StartCameraShake));
            cam.localPosition = Vector3.zero;
        }
    }
}