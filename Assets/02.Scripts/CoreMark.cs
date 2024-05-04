using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreMark : MonoBehaviour
{
    public GameObject coremarkPrefab; // coremark 프리팹을 연결할 변수

    void Start()
    {
        // core의 위치 가져오기
        Vector3 corePosition = transform.position;

        // y축으로 10 위에 있는 위치 계산
        corePosition.y += 4.8f;

        // coremark 프리팹을 위에서 계산한 위치에 생성
        GameObject coremark = Instantiate(coremarkPrefab, corePosition, Quaternion.identity);
    }
}
