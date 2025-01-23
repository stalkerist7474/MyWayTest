using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class AssetBundleLoader : IGameSystem, IEventSubscriber<StartUpdateAssetBundleJsonEvent>
{
    public static AssetBundleLoader Instance;
    [SerializeField] private string bundleUrl;
    [SerializeField] private int version = 0;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [Space]
    [Space]
    [SerializeField] private string namefileSprite = "siamese-cat-lying-down-md.png";
    private string resourcePath = "Assets/Resources";


    public void Subscribe()
    {
        EventBus.RegisterTo(this as IEventSubscriber<StartUpdateAssetBundleJsonEvent>);
    }
    public void Unsubscribe()
    {
        EventBus.UnregisterFrom(this as IEventSubscriber<StartUpdateAssetBundleJsonEvent>);
    }
    public void OnEvent(StartUpdateAssetBundleJsonEvent eventName)
    {
        if (eventName.NeedLoadAssetBundle)
        {
            Debug.Log("StartUpdateAssetBundleJsonEvent in AssetBundleLoader");
            UpdateCurrentAssetBundleSprite();
        }
    }

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
    private void Start()
    {
        Subscribe();
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
            EventBus.RaiseEvent(new LoadNewVersionAssetBundleEvent(sprite, namefileSprite));

            bundle.Unload(true);

        }

        IsActivateComplete = true;
    }


    private Sprite GetCurrentAssetBundleSprite()
    {
        if (spriteRenderer.sprite != null)
        {
            return spriteRenderer.sprite;
        }
        return null;
    }

    private void UpdateCurrentAssetBundleSprite()
    {
        IsActivateComplete = false;
        StartCoroutine(LoadAssetBundle());

    }
    private void OnDestroy()
    {
        Unsubscribe();
    }

}
