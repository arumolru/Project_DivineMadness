using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeController : MonoBehaviour
{
    public CameraShake cameraShake;
    public float minShakeInterval = 5f; // 최소 카메라 쉐이크 간격
    public float maxShakeInterval = 10f; // 최대 카메라 쉐이크 간격

    private float nextShakeTime;

    void Start()
    {
        // 초기 카메라 쉐이크 시간 설정
        SetNextShakeTime();
    }

    void Update()
    {
        // 현재 시간이 다음 카메라 쉐이크 시간보다 크거나 같으면 카메라를 흔들입니다.
        if (Time.time >= nextShakeTime)
        {
            cameraShake.ShakeCamera(3.0f, 0.02f); // 원하는 지속 시간과 강도를 설정하세요.
            SetNextShakeTime(); // 다음 카메라 쉐이크 시간 설정
        }
    }

    // 다음 카메라 쉐이크 시간을 설정하는 함수
    void SetNextShakeTime()
    {
        nextShakeTime = Time.time + Random.Range(minShakeInterval, maxShakeInterval);
    }
}
