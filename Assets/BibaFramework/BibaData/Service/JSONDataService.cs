using LitJson;
using System.IO;
using UnityEngine;
using System;
using BibaFramework.BibaGame;

namespace BibaFramework.BibaData
{
    public class JSONDataService : IDataService
    {
        [Inject]
        public BibaGameModel BibaGameModel { get; set; }

        public void WriteGameModel ()
        {
            WriteToDisk(BibaGameModel, BibaDataConstants.GAME_MODEL_DATA_PATH);
        }

        public BibaFramework.BibaGame.BibaGameModel ReadGameModel ()
        {
            return ReadFromDisk<BibaGameModel>(BibaDataConstants.GAME_MODEL_DATA_PATH);
        }

        public void WriteToDisk<T>(T objectToWrite, string path)
        {
            path = Path.Combine(Application.persistentDataPath, path);
            Debug.Log("Writing: " + path);
            
            var jsonStr = JsonMapper.ToJson(objectToWrite);
            File.WriteAllText(path, jsonStr);
        }

        public T ReadFromDisk<T>(string path)
        {
            path = Path.Combine(Application.persistentDataPath, path);
            Debug.Log("Reading: " + path);

            if (File.Exists(path))
            {
                return JsonMapper.ToObject<T>(File.ReadAllText(path));
            }

            return Activator.CreateInstance<T>(); 
        }
    }
}

