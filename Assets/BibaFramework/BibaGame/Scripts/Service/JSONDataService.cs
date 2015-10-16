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

        public void WriteGameModel ()
        {
            WriteToDisk(BibaGameModel, BibaDataConstants.GAME_MODEL_DATA_PATH);
        }

        public BibaGameModel ReadGameModel ()
        {
            BibaGameModel = ReadFromDisk<BibaGameModel>(BibaDataConstants.GAME_MODEL_DATA_PATH);
            return BibaGameModel;
        }

        public void WriteToDisk<T>(T objectToWrite, string path)
        {
            path = Path.Combine(Application.persistentDataPath, path);
            Debug.Log(string.Format("Writing: {0} with Hashcode: {1} to Path:{2}", objectToWrite.GetType().Name, objectToWrite.GetHashCode(), path));
            
            var jsonStr = JsonMapper.ToJson(objectToWrite);
            File.WriteAllText(path, jsonStr);
        }

        public T ReadFromDisk<T>(string path)
        {
            path = Path.Combine(Application.persistentDataPath, path);
            Debug.Log(string.Format("Reading: {0}", path));

            if (File.Exists(path))
            {
                return JsonMapper.ToObject<T>(File.ReadAllText(path));
            }

            return Activator.CreateInstance<T>(); 
        }
    }
}

