
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : IGameSystem,IEventSubscriber<NewGameStateEvent>
{

    public static GameManager Instance;
    private GameState previusGameState = GameState.Boot;
    private GameState currentGameState;

    public GameState CurrentGameState { get => currentGameState;}

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void Subscribe()
    {
        EventBus.RegisterTo(this as IEventSubscriber<NewGameStateEvent>);
    }
    public void Unsubscribe()
    {
        EventBus.UnregisterFrom(this as IEventSubscriber<NewGameStateEvent>);
    }
    public void OnEvent(NewGameStateEvent eventName)
    {
        ChangeGameState(eventName.NewState);
    }
    private void Start()
    {
        Subscribe();

        IsActivateComplete = true;
    }
    public override async Task Activate()
    {
        Debug.Log("Activate GameManager Start");
        Task task = Task.Run(() =>
        {

         this.gameObject.SetActive(true);

        });

        await task;
    }


    //обработка нового GameState и выполнение необходимых методов
    private void ChangeGameState(GameState newGameState)
    {
        previusGameState = currentGameState;
        currentGameState = newGameState;

        switch (currentGameState)
        {
            case GameState.Menu:
                StartMainMenu();
                break;

            case GameState.Boot:
                //
                break;

            case GameState.GamePlay:
                StartGamePlay();
                break;

            default:
                break;
        }
    }


    private void StartGamePlay()
    {
        SceneManager.LoadSceneAsync(Scenes.GAMEPLAY);
    }

    private void StartMainMenu()
    {
        SceneManager.LoadSceneAsync(Scenes.MENU);
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }

}
