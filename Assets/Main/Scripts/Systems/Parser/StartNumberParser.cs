using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartNumberParser : JsonParser
{
    protected override void ProcessJson(string jsonText)
    {
        if (string.IsNullOrEmpty(jsonText))
        {
            Debug.LogError("JSON text is empty or null.");
            return;
        }

        StartNumber myData = JsonUtility.FromJson<StartNumber>(jsonText);
        Debug.Log($"Parsed JSON: {myData.startingNumber}");

        IsComplete = true;

    }
}
[System.Serializable]
public class StartNumber
{
    public int startingNumber;
}
