using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.IO;

public class JsonParser : MonoBehaviour
{
    [SerializeField] private string jsonUrl;
    [SerializeField] private string localPath;

    public bool IsComplete = false;

    public void LoadJsonFromUrl()
    {
        if (string.IsNullOrEmpty(jsonUrl))
        {
            Debug.LogError("JSON URL NULL!");
            return;
        }

        StartCoroutine(LoadJsonCoroutine());
    }

    protected IEnumerator LoadJsonCoroutine()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(jsonUrl))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error downloading JSON: {webRequest.error}");
            }
            else
            {
                string jsonText = webRequest.downloadHandler.text;

                if (!string.IsNullOrEmpty(localPath))
                {
                    SaveJsonToLocal(jsonText);
                }
                ProcessJson(jsonText);
            }
        }
    }
    protected void SaveJsonToLocal(string jsonText)
    {
        string fullPath = Path.Combine(Application.persistentDataPath, localPath);
        try
        {
            string directory = Path.GetDirectoryName(fullPath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            File.WriteAllText(fullPath, jsonText);
            Debug.Log($"JSON saved to: {fullPath}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error saving JSON: {e.Message}");
        }
    }

    protected virtual void ProcessJson(string jsonText)
    {
        

    }
}
