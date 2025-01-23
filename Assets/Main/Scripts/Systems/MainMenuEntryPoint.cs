using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//����� ����� � ����� � ������� ������������� ����������� ��������� ������� ��������/����������
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
