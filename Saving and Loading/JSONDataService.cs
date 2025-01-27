using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class JSONDataService : IDataService
{
    
    public T LoadData<T>(string relativePath)
    {
        string path = Application.persistentDataPath + relativePath;

        if (!File.Exists(path))
        {
            Debug.LogError("File not found at " + path);
            throw new FileNotFoundException(path + " does not exist");
        }
        try
        {
            T data = JsonConvert.DeserializeObject<T>(File.ReadAllText(path), new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });
            return data;
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to load due to:" + e.Message + e.StackTrace);
            throw;
        }
    }

    public bool SaveData<T>(string relativePath, T saveData)
    {
        string path = Application.persistentDataPath + relativePath;
        
            try
            {
                if (File.Exists(path))
                {
                    Debug.Log("File Exists, deleting old file.");
                    File.Delete(path);
                }
                using FileStream stream = File.Create(path);
                stream.Close();
            File.WriteAllText(path, JsonConvert.SerializeObject(saveData, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            })); ;
                return true;
            }
            catch (Exception e)
            {
                Debug.Log("Error saving data due to " + e.Message + e.StackTrace);
                return false;
            }
    }
}
