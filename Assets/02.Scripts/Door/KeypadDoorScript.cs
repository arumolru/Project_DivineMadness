using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadDoorScript : MonoBehaviour
{
    public Animator animator;
    private bool isDoorOpened = false; // 문이 열렸는지 여부

    public void OpenDoor2()
    {
        if (!isDoorOpened)
        {
            animator.SetTrigger("Open");
            isDoorOpened = true;
        }
    }
}
