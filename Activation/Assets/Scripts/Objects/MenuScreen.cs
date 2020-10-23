using ProjectReversing.Interfaces;
using ProjectReversing.Traits;
using UnityEngine;
namespace ProjectReversing.Objects
{
    public class MenuScreen : MonoBehaviour, IInteractable
    {
        public GameObject menuCam;
        public GameObject playerObject;
        public Canvas menuCanvas;
        public void Interact()
        {
            //Debug.Log("Interacting...");
            menuCanvas.worldCamera = menuCam.GetComponent<Camera>();
            menuCam.SetActive(true);
            playerObject.SetActive(false);
            PlayerUI.singleton.isPaused = true;
        }
        public void Uninteract()
        {
            menuCanvas.worldCamera = null;
            menuCam.SetActive(false);
            playerObject.SetActive(true);
            PlayerUI.singleton.isPaused = false;
        }
        public void OpenLink(string _linkURL)
        {
            Application.OpenURL(_linkURL);
        }
    }
}