
using UnityEngine;

public class ButtonChangeGameState : MonoBehaviour
{
    [SerializeField] private GameState gameStateButton;

    public void ChangeGameState()
    {
        EventBus.RaiseEvent(new NewGameStateEvent(GameManager.Instance.CurrentGameState, gameStateButton));
    }
}
