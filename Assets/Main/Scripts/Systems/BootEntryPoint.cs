using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootEntryPoint : MonoBehaviour
{
    [SerializeField] private UICanvas UIStartGame;
    [SerializeField] private List<IGameSystem> gameSystems;


    private IEnumerator Start()
    {
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        UIStartGame.SwitchOnCanvas();


        foreach (var system in gameSystems) 
        {
            system.Activate();

            while (!system.IsActivateComplete)
            {
                yield return new WaitForSeconds(0.1f); 
            }
            
            Debug.Log($"<color=#20C30C>Load System = {system.gameObject.name}</color>");
        }


        yield return new WaitForSeconds(2f); 

        yield return SceneManager.LoadSceneAsync(Scenes.MENU);


    }

}
