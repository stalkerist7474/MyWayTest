using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


//обновление значения показателя золота на UI на основе событий
public class CoinBar : MonoBehaviour, IEventSubscriber<ChangeCoinValueEvent>
{
    [SerializeField] private TMP_Text valueCoin;
    [Header("Show income parametrs")]
    [SerializeField] private GameObject prefabIncomeCoin;
    [SerializeField] private Transform PointIncomeCoin;

    [SerializeField] private float maxXDistance = 150f;
    [SerializeField] private float maxYDistance = 150f;


    public void Subscribe()
    {
        EventBus.RegisterTo(this as IEventSubscriber<ChangeCoinValueEvent>);
    }
    public void Unsubscribe()
    {
        EventBus.UnregisterFrom(this as IEventSubscriber<ChangeCoinValueEvent>);
    }

    public void OnEvent(ChangeCoinValueEvent eventName)
    {
        UpdateTextCoinValue(eventName.NewValueCoin);

        if (eventName.IsShowIncome)
        {
            ShowIncomeCoin(eventName.NewIncomeCoinValue, eventName.IsExtra);
        }
    }

    //отображение нового значение золота
    private void UpdateTextCoinValue(int value)
    {
        valueCoin.text = value.ToString();
    }

    //отображение пополнения золота, используется пул объектов ( настраивается число, позиция, вращение и родитель для корректной работы слоев)
    private void ShowIncomeCoin(int value, bool isExtra)
    {
        GameObject newIncome = ObjectPool.GetNextObject(prefabIncomeCoin, false);
        FXAddCoinValue FX = newIncome.GetComponent<FXAddCoinValue>();

        FX.coinValue.text = "+" + value.ToString();
        newIncome.transform.SetParent(transform, false);
        newIncome.transform.position = GetRandomPosition();
        newIncome.transform.rotation = Quaternion.Euler(0, 0, Random.Range(1, 30f));
        newIncome.SetActive(true);
        FX.StartShow(isExtra);
    }

    //получение случайной координаты от изначальной точки для показа поступающих денег
    private Vector3 GetRandomPosition()
    {
        float randomX = Random.Range(-maxXDistance, maxXDistance);
        float randomY = Random.Range(-maxYDistance, maxYDistance);

        // Создаем позицию объекта
        Vector3 position = PointIncomeCoin.position + new Vector3(randomX, randomY, 0);
        return position;
    }

    private void Start()
    {
        Subscribe();
    }
    private void OnDestroy()
    {
        Unsubscribe();
    }
}
