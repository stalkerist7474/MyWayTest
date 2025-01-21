
public struct ChangeCoinValueEvent : IEvent
{
    public int NewValueCoin;
    public bool IsShowIncome;
    public int NewIncomeCoinValue;
    public bool IsExtra;

    public ChangeCoinValueEvent(int newValueCoin, bool isShowIncome = false, int newIncomeCoinValue = 0, bool isExtra = false)
    {
        NewValueCoin = newValueCoin;
        IsShowIncome = isShowIncome;
        NewIncomeCoinValue = newIncomeCoinValue;
        IsExtra = isExtra;
    }
}
