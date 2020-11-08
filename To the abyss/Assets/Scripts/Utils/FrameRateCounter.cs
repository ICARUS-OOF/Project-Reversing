using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectReversing.Utils
{
    [RequireComponent(typeof(Text))]
    public class FrameRateCounter : MonoBehaviour
    {
        private Text FPSText;
        public double fpsCounter;
        private void Start()
        {
            FPSText = GetComponent<Text>();
        }
        private void Update()
        {
            fpsCounter = 1f / Time.deltaTime;
            FPSText.text = "FPS: " + fpsCounter.ToString();
        }
    }
}