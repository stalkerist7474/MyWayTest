using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class AssetBundleLoader : IGameSystem
{
    public static AssetBundleLoader Instance;
    [SerializeField] private string bundleUrl;
    [SerializeField] private int version = 0;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [Space]
    [Space]
    [SerializeField] private string namefileSprite = "siamese-cat-lying-down-md.png";
    private string resourcePath = "Assets/Resources";

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public override void Activate()
    {
        StartCoroutine(LoadAssetBundle());

    }

    private IEnumerator LoadAssetBundle()
    {
        while (!Caching.ready)
            yield return null;

        using (UnityWebRequest req = UnityWebRequestAssetBundle.GetAssetBundle(bundleUrl))
        {
            yield return req.SendWebRequest();

            if (req.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to download AssetBundle: " + req.error);
                yield break;
            }

            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(req);
            if (bundle == null)
            {
                Debug.LogError("Failed to load AssetBundle from the download: ");
                yield break;
            }

            var requestAsset = bundle.LoadAssetAsync(namefileSprite, typeof(Sprite));
            yield return requestAsset;


            Sprite sprite = requestAsset.asset as Sprite;
            spriteRenderer.sprite = sprite;
            SaveSpriteToResources(sprite);

        }

        IsActivateComplete = true;
    }

    void SaveSpriteToResources(Sprite sprite)
    {
        string fullAssetPath = Path.Combine(resourcePath, namefileSprite + ".asset");
        if (!Directory.Exists(Path.GetDirectoryName(fullAssetPath)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullAssetPath));
        }
        AssetDatabase.CreateAsset(sprite, fullAssetPath);
        AssetDatabase.SaveAssets();
        Debug.Log("New Sprite created at: " + fullAssetPath);
        
    }


    
}
