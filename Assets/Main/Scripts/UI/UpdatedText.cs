
using UnityEngine;
using TMPro;

public class UpdatedText : MonoBehaviour, IEventSubscriber<LoadedNewJsonDataEvent<HelloMessage>>
{
    [SerializeField] private TMP_Text textHello;


    private void Start()
    {
        Subscribe();
        LoadSaveData();
    }

    public void Subscribe()
    {
        EventBus.RegisterTo(this as IEventSubscriber<LoadedNewJsonDataEvent<HelloMessage>>);
    }
    public void Unsubscribe()
    {
        EventBus.UnregisterFrom(this as IEventSubscriber<LoadedNewJsonDataEvent<HelloMessage>>);
    }

    public void OnEvent(LoadedNewJsonDataEvent<HelloMessage> eventName)
    {
        SaveManager.Instance.MyData.helloMessage = eventName.NewJsonObject.startingMessage;
        SaveManager.Instance.Save();

        textHello.text = SaveManager.Instance.MyData.helloMessage;
    }


    private void LoadSaveData()
    {
        textHello.text = SaveManager.Instance.MyData.helloMessage;
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }
}
