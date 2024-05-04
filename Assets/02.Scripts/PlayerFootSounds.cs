using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootSounds : MonoBehaviour
{
    public AudioClip footstepSound; // 발소리 사운드 클립
    public float footstepInterval = 0.5f; // 발소리 간격

    private AudioSource audioSource;
    private float nextFootstepTime = 0.0f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // 플레이어의 움직임이 발생할 때마다 발소리 재생
        if(Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            if(Time.time>=nextFootstepTime)
            {
                PlayFootstepSound();
                nextFootstepTime = Time.time + footstepInterval;
            }
        }
    }

    void PlayFootstepSound()
    {
        if(footstepSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(footstepSound);
        }
    }
}
