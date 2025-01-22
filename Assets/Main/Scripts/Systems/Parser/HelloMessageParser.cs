using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelloMessageParser : JsonParser
{
    protected override void ProcessJson(string jsonText)
    {
        if (string.IsNullOrEmpty(jsonText))
        {
            Debug.LogError("JSON text is empty or null.");
            return;
        }

        HelloMessage myData = JsonUtility.FromJson<HelloMessage>(jsonText);
        Debug.Log($"Parsed JSON: {myData.startingMessage}");

        IsComplete = true;

    }
}
[System.Serializable]
public class HelloMessage
{
    public string startingMessage;
}