using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    public void TriggerAnimNormalGetCoin()
    {
        animator.SetTrigger("GetNormal");
    }

    public void TriggerAnimExtraGetCoin()
    {
        animator.SetTrigger("GetExtra");
    }
}
