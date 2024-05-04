using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCoreSpawner : MonoBehaviour
{
    public Transform[] redCoreSpawnZones; // Red Core가 생성될 Spawn Zone들의 위치 배열
    public GameObject redCorePrefab; // Red Core 프리팹

    private GameObject lastSpawnedCore; // 마지막으로 생성된 Red Core 오브젝트

    void Start()
    {
        SpawnRedCore();
    }

    void SpawnRedCore()
    {
        // Red Core 생성 전에 이전 Red Core 삭제
        if (lastSpawnedCore != null)
        {
            Destroy(lastSpawnedCore);
        }

        // 랜덤하게 선택된 Spawn Zone에서 Red Core 생성
        int randomIndex = Random.Range(0, redCoreSpawnZones.Length);
        Transform spawnZone = redCoreSpawnZones[randomIndex];

        // Red Core를 spawnZone 위치에 생성
        GameObject spawnedRedCore = Instantiate(redCorePrefab, spawnZone.position, spawnZone.rotation);

        // 마지막으로 생성된 Red Core 오브젝트 저장
        lastSpawnedCore = spawnedRedCore;

        Debug.Log("Red Core가 랜덤 위치에 생성되었습니다.");
    }
}
