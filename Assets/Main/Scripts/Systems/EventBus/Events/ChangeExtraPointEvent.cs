
public struct ChangeExtraPointEvent : IEvent
{
    public float CurrentExtraPoint;

    public ChangeExtraPointEvent(float currentExtraPoint)
    {
        CurrentExtraPoint = currentExtraPoint;
    }
}
