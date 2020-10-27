using ProjectReversing.Data;
using ProjectReversing.Handlers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectReversing.Utils
{
    public class GameDataTransferer : MonoBehaviour
    {
        public void StartGame(string _FileName)
        {
            if (SaveSystem.FileExists(_FileName))
            {
                LoadGame(_FileName);
            }
            else
            {
                GameHandler.SaveFile = _FileName;
            }
        }
        public void LoadGame(string _FileName)
        {
            GameData data = SaveSystem.LoadGame(_FileName);

            Vector3 position;
            position.x = data.position[0];
            position.y = data.position[1];
            position.z = data.position[2];

            GameHandler.SaveFile = _FileName;
            GameHandler.LastCheckPointPos = position;
        }
    }
}