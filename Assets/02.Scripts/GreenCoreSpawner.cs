using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenCoreSpawner : MonoBehaviour
{
    public Transform[] greenCoreSpawnZones; // Green Core가 생성될 Spawn Zone들의 위치 배열
    public GameObject greenCorePrefab; // Green Core 프리팹

    private GameObject lastSpawnedCore; // 마지막으로 생성된 Green Core 오브젝트

    void Start()
    {
        SpawnGreenCore();
    }

    void SpawnGreenCore()
    {
        // Green Core 생성 전에 이전 Green Core 삭제
        if (lastSpawnedCore != null)
        {
            Destroy(lastSpawnedCore);
        }

        // 랜덤하게 선택된 Spawn Zone에서 Green Core 생성
        int randomIndex = Random.Range(0, greenCoreSpawnZones.Length);
        Transform spawnZone = greenCoreSpawnZones[randomIndex];

        // Green Core를 spawnZone 위치에 생성
        GameObject spawnedGreenCore = Instantiate(greenCorePrefab, spawnZone.position, spawnZone.rotation);

        // 마지막으로 생성된 Green Core 오브젝트 저장
        lastSpawnedCore = spawnedGreenCore;

        Debug.Log("Green Core가 랜덤 위치에 생성되었습니다.");
    }
}

