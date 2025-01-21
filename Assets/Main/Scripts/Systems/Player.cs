using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : IGameSystem, IEventSubscriber<PressGetButtonEvent>
{
    [SerializeField] private int valueAddsCoin;
    [SerializeField] private int valueAddsCoinExtra = 500;
    [SerializeField] private Chest playerChest;
    [SerializeField] private float extraPointPerTouch = 0.2f;
    private int coinValue = 0;
    private float extraPoint = 0;


    public override void Activate()
    {
        this.gameObject.SetActive(true);
    }
    public void Subscribe()
    {
        EventBus.RegisterTo(this as IEventSubscriber<PressGetButtonEvent>);
    }
    public void Unsubscribe()
    {
        EventBus.UnregisterFrom(this as IEventSubscriber<PressGetButtonEvent>);
    }

    public void OnEvent(PressGetButtonEvent eventName)
    {
        AddCoin();
        AddPointsToExtra();
    }

    private void Start()
    {
        Subscribe();
        coinValue = SaveManager.Instance.MyData.Coins;
        EventBus.RaiseEvent(new ChangeCoinValueEvent(coinValue));
        StartCoroutine(FadeExtraPoint());
    }

    //добавление монет
    private void AddCoin()
    {
        coinValue += valueAddsCoin;
        playerChest.TriggerAnimNormalGetCoin();
        SaveManager.Instance.MyData.Coins = coinValue;
        SaveManager.Instance.Save();
        EventBus.RaiseEvent(new ChangeCoinValueEvent(coinValue,true, valueAddsCoin,false));
    }


    //добавление очков заполнености слайдера и при достижении полного заполнение = 1 даем спец награду
    private void AddPointsToExtra()
    {
        extraPoint += extraPointPerTouch;

        if ( extraPoint >= 1 )
        {
            extraPoint = 0;
            coinValue += valueAddsCoinExtra;
            SaveManager.Instance.MyData.Coins = coinValue;
            SaveManager.Instance.Save();
            EventBus.RaiseEvent(new ChangeCoinValueEvent(coinValue, true, valueAddsCoinExtra, true));
            playerChest.TriggerAnimExtraGetCoin();
        }
    }


    //корутина для снижения очков с течением времени. также отправляет событие о текущей заполненности слайдера
    private IEnumerator FadeExtraPoint()
    {  
        while(true)
        {
            if(extraPoint > 0)
            {
                extraPoint -= 0.1f;
            }
            EventBus.RaiseEvent(new ChangeExtraPointEvent(extraPoint));
            yield return new WaitForSeconds(0.2f);
        }
        

    }

    private void OnDestroy()
    {
        Unsubscribe();
    }
}
