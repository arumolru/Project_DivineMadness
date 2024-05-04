using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectionScript : MonoBehaviour
{
    public DoorClose doorClose; // DoorClose 스크립트 참조

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorClose.CloseDoor();
        }
    }
}
