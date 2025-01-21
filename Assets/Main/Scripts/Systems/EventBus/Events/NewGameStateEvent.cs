

//евент нового состояния игры

public struct NewGameStateEvent : IEvent
{
    public GameState OldState;
    public GameState NewState;


    public NewGameStateEvent(GameState oldState, GameState newState)
    {
        OldState = oldState;
        NewState = newState;

    }
}
