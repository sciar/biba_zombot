using LitJson;
using System.IO;
using UnityEngine;
using System;

namespace BibaFramework.BibaGame
{
    public class JSONDataService : IDataService
    {
        [Inject]
        public BibaGameModel BibaGameModel { get; set; }

        [Inject]
        public GameModelUpdatedSignal GameModelUpdatedSignal { get; set; }

        public void WriteGameModel ()
        {
            var path = Path.Combine(Application.persistentDataPath, BibaDataConstants.GAME_MODEL_DATA_PATH);

            WriteToDisk(BibaGameModel, path);
            GameModelUpdatedSignal.Dispatch();
        }

        public BibaGameModel ReadGameModel ()
        {
            var path = Path.Combine(Application.persistentDataPath, BibaDataConstants.GAME_MODEL_DATA_PATH);

            BibaGameModel = ReadFromDisk<BibaGameModel>(path);
            return BibaGameModel;
        }

        public void WriteToDisk<T>(T objectToWrite, string path)
        {
            Debug.Log(string.Format("Writing: {0} with Hashcode: {1} to Path:{2}", objectToWrite.GetType().Name, objectToWrite.GetHashCode(), path));

            if(!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }

            var jsonStr = JsonMapper.ToJson(objectToWrite);
            File.WriteAllText(path, jsonStr);
        }

        public T ReadFromDisk<T>(string path)
        {
            Debug.Log(string.Format("Reading: {0}", path));

            //Check if file is accessible in the fileSystem
            if (File.Exists(path))
            {
                return JsonMapper.ToObject<T>(File.ReadAllText(path));
            }

            //Check in the Resources folder if not found in the file system
            var textAsset = Resources.Load<TextAsset>(Path.GetFileNameWithoutExtension(path));
            if (textAsset != null && !string.IsNullOrEmpty(textAsset.text))
            {
                return JsonMapper.ToObject<T>(textAsset.text);
            }

            return Activator.CreateInstance<T>(); 
        }
    }
}

