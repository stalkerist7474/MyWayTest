using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootEntryPoint : MonoBehaviour
{
    [SerializeField] private UICanvas UIStartGame;
    [SerializeField] private List<IGameSystem> gameSystems;




    private void Start()
    {
        StartActivate();
    }



    private async Task StartActivate()
    {
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        UIStartGame.SwitchOnCanvas();

        List<Task> activationTasks = new List<Task>();

        foreach (var system in gameSystems)
        {
            activationTasks.Add(system.Activate());
            Debug.Log($"<color=#20C30C>start activation system{system.gameObject.name} </color>");
        }

        await Task.WhenAll(activationTasks);

        Debug.Log("<color=#20C30C>All systems on start activated!</color>");

        await Task.Delay(TimeSpan.FromSeconds(2));


        SceneManager.LoadSceneAsync(Scenes.MENU);


    }

}
