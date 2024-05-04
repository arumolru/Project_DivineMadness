using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorClose : MonoBehaviour
{
    public Animator animator;
    private bool isDoorClosed = false; // 문이 닫혔는지 여부

    public void CloseDoor()
    {
        if (!isDoorClosed)
        {
            animator.SetTrigger("Close");
            isDoorClosed = true;
        }
    }
}
