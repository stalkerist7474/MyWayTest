using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


//���������� �������� ���������� ������ �� UI �� ������ �������
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

    //����������� ������ �������� ������
    private void UpdateTextCoinValue(int value)
    {
        valueCoin.text = value.ToString();
    }

    //����������� ���������� ������, ������������ ��� �������� ( ������������� �����, �������, �������� � �������� ��� ���������� ������ �����)
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

    //��������� ��������� ���������� �� ����������� ����� ��� ������ ����������� �����
    private Vector3 GetRandomPosition()
    {
        float randomX = Random.Range(-maxXDistance, maxXDistance);
        float randomY = Random.Range(-maxYDistance, maxYDistance);

        // ������� ������� �������
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
