using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//����� ����� � ����� � ������� ������������� ����������� ��������� ������� ��������/����������
public class BootEntryPoint : MonoBehaviour
{
    [SerializeField] private UICanvas UIStartGame;
    [SerializeField] private List<IGameSystem> gameSystems;


    private IEnumerator Start()
    {
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        UIStartGame.SwitchOnCanvas();


        foreach (var system in gameSystems) // ����������� ��������/��������� ������ ���� ������� ����� �������� �� ���������� ����� �������� ��������(savemanager,firebase, �������, InApp, � ������ SDK � ������� 
        {
            system.Activate();
            Debug.Log($"<color=#20C30C>Load System = {system.gameObject.name}</color>");
        }

#if UNITY_EDITOR
#else
        InitOrientation();
#endif


        yield return new WaitForSeconds(2f); //�������� �������� �� 2 ������� �� ��

        yield return SceneManager.LoadSceneAsync(Scenes.MENU);


    }


    private void InitOrientation()
    {
        if(SaveManager.Instance.MyData.OrientationGame == OrientationGame.Horizontal)
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }
        if (SaveManager.Instance.MyData.OrientationGame == OrientationGame.Vertical)
        {
            Screen.orientation = ScreenOrientation.Portrait;
        }

    }
}
