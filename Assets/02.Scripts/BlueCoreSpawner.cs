using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueCoreSpawner : MonoBehaviour
{
    public Transform[] blueCoreSpawnZones; // Blue Core가 생성될 Spawn Zone들의 위치 배열
    public GameObject blueCorePrefab; // Blue Core 프리팹

    private GameObject lastSpawnedCore; // 마지막으로 생성된 Blue Core 오브젝트

    void Start()
    {
        SpawnBlueCore();
    }

    void SpawnBlueCore()
    {
        // Blue Core 생성 전에 이전 Blue Core 삭제
        if (lastSpawnedCore != null)
        {
            Destroy(lastSpawnedCore);
        }

        // 랜덤하게 선택된 Spawn Zone에서 Blue Core 생성
        int randomIndex = Random.Range(0, blueCoreSpawnZones.Length);
        Transform spawnZone = blueCoreSpawnZones[randomIndex];

        // Blue Core를 spawnZone 위치에 생성
        GameObject spawnedBlueCore = Instantiate(blueCorePrefab, spawnZone.position, spawnZone.rotation);

        // 마지막으로 생성된 Blue Core 오브젝트 저장
        lastSpawnedCore = spawnedBlueCore;

        Debug.Log("Blue Core가 랜덤 위치에 생성되었습니다.");
    }
}
