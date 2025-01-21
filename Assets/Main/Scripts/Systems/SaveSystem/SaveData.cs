using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

[Serializable]
public class SaveData
{
    [Header("Main parametrs")]
    [ReadOnly] public int Coins = 0;
    [ReadOnly] public OrientationGame OrientationGame = OrientationGame.Horizontal;
    
}