using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParseSystem : IGameSystem, IEventSubscriber<StartUpdateAssetBundleJsonEvent>
{
    public static ParseSystem Instance;
    [SerializeField] List<JsonParser> listParser;

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
        if (eventName.NeedLoadJson)
        {
            Debug.Log("StartUpdateAssetBundleJsonEvent in ParseSystem");
            StartCoroutine(LoadJson());
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
        StartCoroutine(LoadJson());
        
    }

    private IEnumerator LoadJson()
    {

        foreach (var item in listParser)
        {
            item.LoadJsonFromUrl();
            while (!item.IsComplete)
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
        IsActivateComplete = true;
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }
}
