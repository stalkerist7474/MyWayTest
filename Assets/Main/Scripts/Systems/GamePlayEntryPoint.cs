using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//����� ����� � ����� � ������� ������������� ����������� ��������� ������� ��������/����������
public class GamePlayEntryPoint : MonoBehaviour
{
    [SerializeField] private List<IGameSystem> gameSystems;

    private void Start()
    {
        foreach (var system in gameSystems) // ����������� ��������/��������� ������ ���� ������� ����� �������� �� ���������� ����� �������� ��������(savemanager,firebase, �������, InApp, � ������ SDK � ������� 
        {
            system.Activate();
            Debug.Log($"<color=#20C30C>Load System GamePlay = {system.gameObject.name}</color>");
        }

    }

}
