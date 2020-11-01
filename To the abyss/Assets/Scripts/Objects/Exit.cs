using ProjectReversing.Handlers;
using ProjectReversing.Interfaces;
using ProjectReversing.Traits;
using UnityEngine;
namespace ProjectReversing.Objects
{
    public class Exit : MonoBehaviour, IInteractable
    {
        [SerializeField] GameObject ExitGameUI;
        public void Interact()
        {
            ExitGameUI.SetActive(true);
            PlayerUI.singleton.Pause();
        }
        public void Uninteract()
        {
            ExitGameUI.SetActive(false);
            PlayerUI.singleton.Resume();
        }
        public void ExitGame()
        {
            Debug.Log("Exitting game...");
            Application.Quit();
        }
    }
}