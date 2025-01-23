
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
        EventBus.RaiseEvent(new LoadedNewJsonDataEvent<StartNumber>(myData));
    }
}
[System.Serializable]
public class StartNumber
{
    public int startingNumber;
}
