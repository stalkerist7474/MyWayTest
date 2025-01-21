using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonSaveSystem : ISaveSystem
{
    private string _filePath;


    public JsonSaveSystem()
    {
        _filePath = Application.persistentDataPath + "/Save.json";
        //Debug.Log("JsonSaveSystem at: " + _filePath);
    }


    public void Save(SaveData data)
    {
        //Debug.Log("Saving SaveData to JSON file");
        var json = JsonUtility.ToJson(data);    
        using (var writer = new StreamWriter(_filePath))
        {
            writer.WriteLine(json);
        }
        //Debug.Log("Save COMPLETE to JSON file");
    }

    public SaveData Load()
    {
        string json = "";
        if(File.Exists(_filePath)) 
        {

            //Debug.Log(DateTime.UtcNow.ToString());
            //Debug.Log("Loading SaveData From JSON file");
            using (var reader = new StreamReader(_filePath))
            {
                string line;
                while((line = reader.ReadLine()) != null)
                {
                    json += line;
                }
                //Debug.Log("Load COMPLETE From JSON file");
            }
            if (string.IsNullOrEmpty(json))
            {
                //Debug.Log("Load NOT COMPLETE making new save data / JSON file IsNullOrEmpty");
                return new SaveData();
            }
        }
        else
        {
            //Debug.Log("No Save.json file");
            using (var writer = new StreamWriter(_filePath))
            {
                writer.WriteLine(json);
            }
            //Debug.Log("Create Save.json file");
            if (string.IsNullOrEmpty(json))
            {
                //Debug.Log("Load NOT COMPLETE making new save data / JSON file IsNullOrEmpty");
                return new SaveData();
            }
            //Debug.Log("Fill Save.json file");
        }

        return JsonUtility.FromJson<SaveData>(json);
    }

}
