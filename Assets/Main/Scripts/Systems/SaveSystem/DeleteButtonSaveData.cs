using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteButtonSaveData : MonoBehaviour
{
    public void DeleteSave()
    {
        SaveManager.Instance.SetDefaultSaveData();
    }
}
