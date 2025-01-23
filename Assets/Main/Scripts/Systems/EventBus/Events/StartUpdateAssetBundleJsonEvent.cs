
public struct StartUpdateAssetBundleJsonEvent : IEvent
{
    public bool NeedLoadAssetBundle;
    public bool NeedLoadJson;


    public StartUpdateAssetBundleJsonEvent(bool needLoadAssetBundle = true, bool needLoadJson = true)
    {
        NeedLoadAssetBundle = needLoadAssetBundle;
        NeedLoadJson = needLoadJson;

    }
}
