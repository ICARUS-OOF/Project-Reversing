using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ProjectReversing.Handlers;

namespace ProjectReversing.Data
{
    public static class SaveSystem
    {
        public static void SaveGame(GameHandler handler, string saveName)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/" + saveName +".saved";

            FileStream stream = new FileStream(path, FileMode.Create);

            GameData data = new GameData(handler);

            formatter.Serialize(stream, data);
            stream.Close();
        }
        public static GameData LoadGame(string saveName)
        {
            string path = Application.persistentDataPath + "/" + saveName + ".saved";
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                GameData data = formatter.Deserialize(stream) as GameData;
                stream.Close();
                
                return data;
            } else
            {
                Debug.LogError("Save file not found in" + path);
                return null;
            }
        }
        public static bool FileExists(string saveName)
        {
            string path = Application.persistentDataPath + "/" + saveName + ".saved";
            return File.Exists(path);
        }
        public static void DeleteSave(string saveName)
        {
            string path = Application.persistentDataPath + "/" + saveName + ".saved";
            if (FileExists(saveName))
            {
                File.Delete(path);
            }
        }
    }
}