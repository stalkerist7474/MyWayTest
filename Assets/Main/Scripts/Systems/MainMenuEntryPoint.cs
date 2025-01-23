using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//точка входа в сцену в которой настраивается очередность включения игровых объектов/менеджеров
public class MainMenuEntryPoint : MonoBehaviour
{
    [SerializeField] private List<IGameSystem> gameSystems;


    private void Start()
    {
        foreach (var system in gameSystems) 
        {
            system.Activate();
            Debug.Log($"<color=#20C30C>Load System Main menu = {system.gameObject.name}</color>");
        }
    }
}
