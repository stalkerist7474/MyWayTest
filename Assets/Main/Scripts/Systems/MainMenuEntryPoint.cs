using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//точка входа в сцену в которой настраивается очередность включения игровых объектов/менеджеров
public class MainMenuEntryPoint : MonoBehaviour
{
    [SerializeField] private List<IGameSystem> gameSystems;


    private void Start()
    {
        foreach (var system in gameSystems) // очередность загрузки/включения систем игры которые будут работать на протяжении всего игрового процесса(savemanager,firebase, реклама, InApp, и другие SDK и системы 
        {
            system.Activate();
            Debug.Log($"<color=#20C30C>Load System Main menu = {system.gameObject.name}</color>");
        }
    }
}
