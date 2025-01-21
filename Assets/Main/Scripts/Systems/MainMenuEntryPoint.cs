using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//����� ����� � ����� � ������� ������������� ����������� ��������� ������� ��������/����������
public class MainMenuEntryPoint : MonoBehaviour
{
    [SerializeField] private List<IGameSystem> gameSystems;


    private void Start()
    {
        foreach (var system in gameSystems) // ����������� ��������/��������� ������ ���� ������� ����� �������� �� ���������� ����� �������� ��������(savemanager,firebase, �������, InApp, � ������ SDK � ������� 
        {
            system.Activate();
            Debug.Log($"<color=#20C30C>Load System Main menu = {system.gameObject.name}</color>");
        }
    }
}
