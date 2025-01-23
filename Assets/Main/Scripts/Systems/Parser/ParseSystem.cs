using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            Activate();
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

    public override async Task Activate()
    {
        List<Task> ParseTasks = new List<Task>();

        foreach (var item in listParser)
        {           
            ParseTasks.Add(item.LoadJsonFromUrl());
        }
        await Task.WhenAll(ParseTasks);

    }


    private void OnDestroy()
    {
        Unsubscribe();
    }
}
