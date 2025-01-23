
using System.Threading.Tasks;
using UnityEngine;

public abstract class IGameSystem : MonoBehaviour
{
    public bool IsActivateComplete = false;
    public abstract Task Activate();
}
