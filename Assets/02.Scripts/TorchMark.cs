using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchMark : MonoBehaviour
{
    public GameObject torchmarkPrefab; // greentorchmark 프리팹을 연결할 변수

    void Start()
    {
        // greentorch의 위치 가져오기
        Vector3 torchPosition = transform.position;

        // y축으로 10 위에 있는 위치 계산
        torchPosition.y += 4.1f;

        // greentorchmark 프리팹을 위에서 계산한 위치에 생성
        GameObject greentorchmark = Instantiate(torchmarkPrefab, torchPosition, Quaternion.identity);
    }
}
