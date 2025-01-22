using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IGameSystem : MonoBehaviour
{
    public bool IsActivateComplete = false;
    public abstract void Activate();
}
