using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ProjectReversing.Setups
{
    public class PortalTexturer : MonoBehaviour
    {
        public Camera cameraA;
        public Material camMatA;

        public Camera cameraB;
        public Material camMatB;

        public MeshRenderer rendererA;
        public MeshRenderer rendererB;

        private void Start()
        {
            camMatA = new Material(Shader.Find("Unlit/ScreenCutoutShader"));
            camMatB = new Material(Shader.Find("Unlit/ScreenCutoutShader"));

            rendererA.material = camMatB;
            rendererB.material = camMatA;

            //Camera A
            if (cameraA.targetTexture != null)
            {
                cameraA.targetTexture.Release();
            }
            cameraA.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
            camMatA.mainTexture = cameraA.targetTexture;

            //Camera B
            if (cameraB.targetTexture != null)
            {
                cameraB.targetTexture.Release();
            }
            cameraB.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
            camMatB.mainTexture = cameraB.targetTexture;
        }
    }
}