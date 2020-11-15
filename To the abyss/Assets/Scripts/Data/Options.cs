using UnityEngine;
using UnityEngine.UI;

namespace ProjectReversing.Data
{
    public class Options : MonoBehaviour
    {
        #region Singleton
        public static Options singleton;
        private void Awake()
        {
            if (singleton == null)
            {
                singleton = this;
            }
        }
        #endregion
        [SerializeField] private Slider sensitvitySlider;
        [SerializeField] private Slider volumeSlider;
        [SerializeField] private Slider gfxSlider;
        public float GetSensitivity()
        {
            return sensitvitySlider.value;
        }
        public float GetVolume()
        {
            return volumeSlider.value;
        }
        public float GetGFX()
        {
            return gfxSlider.value;
        }
    }
}