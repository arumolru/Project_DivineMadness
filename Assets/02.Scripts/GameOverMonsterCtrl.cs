using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameOverMonsterCtrl : MonoBehaviour
{
    public Transform mainCamera; // 메인 카메라를 드래그하여 연결합니다.
    private UnityEngine.AI.NavMeshAgent navMeshAgent;

    void Start()
    {
        mainCamera = Camera.main.transform; // 메인 카메라의 Transform을 가져온다
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        // 몬스터의 추적을 시작하기 전에 3.0초 대기
        Invoke("StartChasing", 3.0f);
    }

    void Update()
    {
        // 추적 시작 후에 메인 카메라를 계속 추격
        if(mainCamera != null)
        {
            navMeshAgent.SetDestination(mainCamera.position);
        }
    }

    // 추적을 시작하는 함수
    void StartChasing()
    {
        navMeshAgent.enabled = true; // 네비게이션을 활성화하여 추적을 시작합니다.
        Debug.Log("추적을 시작합니다."); // 디버그 메시지 출력
    }
}
