using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeCoreSpawner : MonoBehaviour
{
    public Transform[] orangeCoreSpawnZones; // Orange Core가 생성될 Spawn Zone들의 위치 배열
    public GameObject orangeCorePrefab; // Orange Core 프리팹

    private GameObject lastSpawnedCore; // 마지막으로 생성된 Orange Core 오브젝트

    void Start()
    {
        SpawnOrangeCore();
    }

    void SpawnOrangeCore()
    {
        // Orange Core 생성 전에 이전 Orange Core 삭제
        if (lastSpawnedCore != null)
        {
            Destroy(lastSpawnedCore);
        }

        // 랜덤하게 선택된 Spawn Zone에서 Orange Core 생성
        int randomIndex = Random.Range(0, orangeCoreSpawnZones.Length);
        Transform spawnZone = orangeCoreSpawnZones[randomIndex];

        // Orange Core를 spawnZone 위치에 생성
        GameObject spawnedOrangeCore = Instantiate(orangeCorePrefab, spawnZone.position, spawnZone.rotation);

        // 마지막으로 생성된 Orange Core 오브젝트 저장
        lastSpawnedCore = spawnedOrangeCore;

        Debug.Log("Orange Core가 랜덤 위치에 생성되었습니다.");
    }
}
