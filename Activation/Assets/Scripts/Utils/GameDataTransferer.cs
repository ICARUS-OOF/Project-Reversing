using ProjectReversing.Data;
using ProjectReversing.Handlers;
using UnityEngine;
namespace ProjectReversing.Utils
{
    public class GameDataTransferer : MonoBehaviour
    {
        public void LoadGame(string _FileName)
        {
            GameData data = SaveSystem.LoadGame(_FileName);

            Vector3 position;
            position.x = data.position[0];
            position.y = data.position[1];
            position.z = data.position[2];

            GameHandler.LastCheckPointPos = position;
        }
    }
}