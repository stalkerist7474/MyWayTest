using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderExtraCoin : MonoBehaviour, IEventSubscriber<ChangeExtraPointEvent>
{
    [SerializeField] private Slider progressBar;


    public void Subscribe()
    {
        EventBus.RegisterTo(this as IEventSubscriber<ChangeExtraPointEvent>);
    }
    public void Unsubscribe()
    {
        EventBus.UnregisterFrom(this as IEventSubscriber<ChangeExtraPointEvent>);
    }

    private void Start()
    {
        Subscribe();
    }
    public void OnEvent(ChangeExtraPointEvent eventName)
    {
        UpdateSlider(eventName.CurrentExtraPoint);
    }

    //обновление заполнености слайдера 
    private void UpdateSlider(float value)
    {
        progressBar.value = value;
    }


    private void OnDestroy()
    {
        Unsubscribe();
    }

}
