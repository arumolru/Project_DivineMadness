using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchSpawner : MonoBehaviour
{
    public GameObject torchPrefab;  // 횃불 프리팹
    public Transform[] spawnPoints; // 횃불 생성 위치 배열

    private GameObject spawnedTorch; // 생성된 횃불 오브젝트

    private void Start()
    {
        // 생성된 횃불 오브젝트가 없을 때만 생성
        if (spawnedTorch == null)
        {
            // 랜덤한 위치에서 횃불 생성
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomIndex];
            spawnedTorch = Instantiate(torchPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }

    private void Update()
    {
        // 생성된 횃불 오브젝트가 삭제되었을 경우 spawnedTorch 변수를 null로 초기화
        if (spawnedTorch != null && !spawnedTorch.activeSelf)
        {
            spawnedTorch = null;
        }
    }
}
