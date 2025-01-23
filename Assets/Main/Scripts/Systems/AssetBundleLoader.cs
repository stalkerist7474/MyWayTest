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

    public SpriteRenderer SpriteRenderer { get => spriteRenderer; set => spriteRenderer = value; }

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


        UnityWebRequest get = UnityWebRequestAssetBundle.GetAssetBundle(bundleUrl);
        yield return get.SendWebRequest();

        UnityWebRequest www = UnityWebRequest.Get(bundleUrl);
        yield return www.SendWebRequest();

        AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(get);

        System.IO.File.WriteAllBytes(resourcePath + "/" + bundle.name, www.downloadHandler.data);

        bundle.Unload(true);

        yield return www.isDone;

        IsActivateComplete = true;

        StartCoroutine(LoadSprite("backgrondspritebutton"));

    }

    //backgrondspritebutton

    private IEnumerator LoadSprite(string nameAsset)
    {
        AssetBundle assetBundle = AssetBundle.LoadFromFile(resourcePath + "/" + "button" + "/" + nameAsset);

        if (assetBundle == null)
        {
            Debug.LogError("Failed to load AssetBundle!");
            yield break;
        }

        AssetBundleRequest request = assetBundle.LoadAssetAsync<Sprite>(namefileSprite);
        yield return request;

        Sprite loadedSprite = request.asset as Sprite;

        if (loadedSprite == null)
        {
            Debug.LogError("Failed to load Sprite from AssetBundle!");
            yield break;
        }

        SpriteRenderer.sprite = loadedSprite;

        EventBus.RaiseEvent(new LoadNewVersionAssetBundleEvent(SpriteRenderer.sprite, namefileSprite));
        assetBundle.Unload(false);
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
