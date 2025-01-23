
public struct LoadedNewJsonDataEvent<T> : IEvent
{
    public T NewJsonObject;

    public LoadedNewJsonDataEvent(T newJsonObject)
    {
        NewJsonObject = newJsonObject;
    }
}
