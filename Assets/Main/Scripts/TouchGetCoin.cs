using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchGetCoin : MonoBehaviour
{

    public void PressGetCoin()
    {
        EventBus.RaiseEvent(new PressGetButtonEvent());
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended) // ������������ ������ ���������� ����
            {
                ProcessTouch(touch.position);
            }
            return;
        }



        if (Input.GetMouseButtonDown(0)) // ��������� ����� ������ ����, ���� � ����������
        {
            ProcessTouch(Input.mousePosition);
            
        }


    }

    private void ProcessTouch(Vector2 posTouch)
    {
        RaycastHit2D hit = Physics2D.Raycast(posTouch, Vector2.zero);

        if (hit.collider != null && !hit.collider.gameObject.GetComponent<ButtonChangeGameState>()) //�������� �� ���� � ������� �� ������ ���� �� ����
        {
            if (hit.collider.gameObject == gameObject)
            {
                PressGetCoin();
            }
        }
    }
}
