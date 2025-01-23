
using UnityEngine;

public struct LoadNewVersionAssetBundleEvent : IEvent
{
    public Sprite NewSprite;
    public string TagAsset;


    public LoadNewVersionAssetBundleEvent(Sprite newSprite, string tagAsset)
    {
        NewSprite = newSprite;
        TagAsset = tagAsset;
    }
}
