using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonUpdateContent : MonoBehaviour
{
    public void UpdateJsonAndAssetBundles()
    {
        Debug.Log("ButtonUpdateContent click");
        EventBus.RaiseEvent(new StartUpdateAssetBundleJsonEvent(true, true));
    }
}
