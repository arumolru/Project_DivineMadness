using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door1Script : MonoBehaviour
{
    public Animator animator;

    public void OpenDoor1()
    {
        animator.SetTrigger("Open");
    }

}
