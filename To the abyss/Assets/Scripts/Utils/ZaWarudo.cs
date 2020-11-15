using ProjectReversing.Handlers;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
namespace ProjectReversing.Utils
{
    public class ZaWarudo : MonoBehaviour
    {
        private PostProcessVolume PPV;

        [SerializeField, Range(-100f, 100f)] private float lensDistortionIntensity = 0;
        [SerializeField, Range(0f, 1f)] private float chromaticAberrationIntensity = .4f;
        [SerializeField, Range(-180, 180)] private float saturation = 0;

        private LensDistortion lensDistortion;
        private ChromaticAberration chromaticAberration;
        private ColorGrading colorGrading;

        private void Start()
        {
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

            if (Input.GetKeyDown(KeyCode.R))
            {
                GetComponent<Animator>().SetTrigger("Za Warudo");
                AudioHandler.PlaySoundEffect("Za Warudo");
            }
        }
    }
}