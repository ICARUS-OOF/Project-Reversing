using ProjectReversing.Enums;
using ProjectReversing.Handlers;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace ProjectReversing.Objects
{
    public class MenuScreen : MonoBehaviour
    {
        public Menu CurrentMenu;

        public GameObject mainMenu;
        public GameObject optionsMenu;
        public GameObject aboutMenu;

        public Slider sensitivitySlider;
        public Slider volumeSlider;
        public Slider gfxSlider;

        public GameObject LoadingScreen;
        private void Update()
        {
            GameHandler.sensitivity = sensitivitySlider.value;
            GameHandler.volume = volumeSlider.value;
            GameHandler.GFX = gfxSlider.value;

            switch (CurrentMenu)
            {
                case Menu.Main:
                    mainMenu.SetActive(true);
                    optionsMenu.SetActive(false);
                    aboutMenu.SetActive(false);
                    break;
                case Menu.Options:
                    mainMenu.SetActive(false);
                    optionsMenu.SetActive(true);
                    aboutMenu.SetActive(false);
                    break;
                case Menu.About:
                    mainMenu.SetActive(false);
                    optionsMenu.SetActive(false);
                    aboutMenu.SetActive(true);
                    break;
            }
        }
        public void Play()
        {
            LoadingScreen.SetActive(true);
            StartCoroutine(LoadPlayScene());
        }
        IEnumerator LoadPlayScene()
        {
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene("Main");
        }
        public void OpenLink(string _linkURL)
        {
            Application.OpenURL(_linkURL);
        }
        public void SetMenu(int _index)
        {
            CurrentMenu = (Menu)_index;
        }
        public void Exit()
        {
            Debug.Log("Exitting game...");
            Application.Quit();
        }
    }
} 