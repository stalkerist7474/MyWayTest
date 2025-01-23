using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ButtonNumberAdd : MonoBehaviour, 
    IEventSubscriber<LoadNewVersionAssetBundleEvent>, 
    IEventSubscriber<LoadedNewJsonDataEvent<StartNumber>>
{

    [SerializeField] private string tagAssetSprite = "siamese-cat-lying-down-md.png";
    [SerializeField] private TMP_Text textNumber;

    private Button button;
    private int valueNumber;


    private void Awake()
    {
        Subscribe();
        button = GetComponent<Button>();
        LoadSaveData();
    }

    public void Subscribe()
    {
        EventBus.RegisterTo(this as IEventSubscriber<LoadNewVersionAssetBundleEvent>);
        EventBus.RegisterTo(this as IEventSubscriber<LoadedNewJsonDataEvent<StartNumber>>);
    }
    public void Unsubscribe()
    {
        EventBus.UnregisterFrom(this as IEventSubscriber<LoadNewVersionAssetBundleEvent>);
        EventBus.UnregisterFrom(this as IEventSubscriber<LoadedNewJsonDataEvent<StartNumber>>);
    }
    public void OnEvent(LoadNewVersionAssetBundleEvent eventName)
    {
        if (eventName.TagAsset == tagAssetSprite)
        {
            Debug.Log("LoadNewVersionAssetBundleEvent in ButtonNumberAdd");
            button.image.sprite = eventName.NewSprite;
        }
    }
    public void OnEvent(LoadedNewJsonDataEvent<StartNumber> eventName)
    {
        valueNumber = eventName.NewJsonObject.startingNumber;
        SaveManager.Instance.MyData.startingNumber = valueNumber;
        SaveManager.Instance.Save();

        textNumber.text = valueNumber.ToString();
    }

    private void LoadSaveData()
    {
        valueNumber = SaveManager.Instance.MyData.startingNumber;
        textNumber.text = valueNumber.ToString();
    }

    public void ClickButton()
    {
        valueNumber++;
        SaveManager.Instance.MyData.startingNumber = valueNumber;
        SaveManager.Instance.Save();

        textNumber.text = valueNumber.ToString();
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }
}
