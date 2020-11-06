using ProjectReversing.Data;
using ProjectReversing.Enums;
using ProjectReversing.Handlers;
using ProjectReversing.Utils;
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
        public GameObject LoadingMenu;

        public Referencer[] Saves;

        public Slider sensitivitySlider;
        public Slider volumeSlider;
        public Slider gfxSlider;

        public GameObject LoadingScreen;

        public GameDataTransferer transferer;
        private void Update()
        {
            GameHandler.sensitivity = sensitivitySlider.value;
            GameHandler.volume = volumeSlider.value;
            GameHandler.GFX = gfxSlider.value;

            for (int i = 0; i < Saves.Length; i++)
            {
                if (SaveSystem.FileExists(Saves[i].transform.name))
                {
                    Saves[i].refObject.GetComponent<Text>().text = "Last played: " + SaveSystem.LoadGame(Saves[i].transform.name).lastPlayedTime.ToString();
                    Saves[i].refObject2.SetActive(true);
                } else
                {
                    Saves[i].refObject.GetComponent<Text>().text = "Empty Save";
                    Saves[i].refObject2.SetActive(false);
                }
            }

            switch (CurrentMenu)
            {
                case Menu.Main:
                    mainMenu.SetActive(true);
                    optionsMenu.SetActive(false);
                    aboutMenu.SetActive(false);
                    LoadingMenu.SetActive(false);
                    break;
                case Menu.Options:
                    mainMenu.SetActive(false);
                    optionsMenu.SetActive(true);
                    aboutMenu.SetActive(false);
                    LoadingMenu.SetActive(false);
                    break;
                case Menu.About:
                    mainMenu.SetActive(false);
                    optionsMenu.SetActive(false);
                    aboutMenu.SetActive(true);
                    LoadingMenu.SetActive(false);
                    break;
                case Menu.Loading:
                    mainMenu.SetActive(false);
                    optionsMenu.SetActive(false);
                    aboutMenu.SetActive(false);
                    LoadingMenu.SetActive(true);
                    break;
            }
        }
        public void Play(string _FileName)
        {
            transferer.StartGame(_FileName);
            LoadingScreen.SetActive(true);
            StartCoroutine(LoadPlayScene());
        }
        IEnumerator LoadPlayScene()
        {
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene("Main");
        }
        public void DeleteSave(string _FileName)
        {
            SaveSystem.DeleteSave(_FileName);
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