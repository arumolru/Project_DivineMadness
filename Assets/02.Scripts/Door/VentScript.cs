using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentScript : MonoBehaviour
{
    public Animator animator;

    public void OpenVent()
    {
        animator.SetTrigger("Open");
    }
}
