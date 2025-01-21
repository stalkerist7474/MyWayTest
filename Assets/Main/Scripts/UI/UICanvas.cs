using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvas : MonoBehaviour
{
    [SerializeField] private GameObject CanvasGameobject;


    private void Awake()
    {
        SwitchOffCanvas();
    }

    public void SwitchOnCanvas()
    {
        CanvasGameobject.SetActive(true);
    }

    public void SwitchOffCanvas()
    {
        CanvasGameobject.SetActive(false);
    }
}
