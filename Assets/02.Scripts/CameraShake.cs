using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Transform camTransform; // 카메라의 Transform
    public float shakeDuration = 10.0f; // 흔들림 지속 시간
    public float shakeMagnitude = 0.7f; // 흔들림 강도
    private Vector3 originalPosition; // 초기 카메라 위치

    void Awake()
    {
        camTransform = GetComponent<Transform>();
    }

    void OnEnable()
    {
        originalPosition = camTransform.localPosition;
    }

    void Update()
    {
        if(shakeDuration>0)
        {
            camTransform.localPosition = originalPosition + Random.insideUnitSphere * shakeMagnitude;

            shakeDuration -= Time.deltaTime;
        }
        else
        {
            shakeDuration = 0f;
            camTransform.localPosition = originalPosition;
        }
    }

    public void ShakeCamera(float duration,float magnitude)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
    }
}
