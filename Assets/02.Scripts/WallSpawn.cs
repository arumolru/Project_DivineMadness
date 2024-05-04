using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawn : MonoBehaviour
{
    public GameObject wallPrefab; // 생성될 벽 프리팹
    public Transform wallSpawnZone; // 벽이 생성될 위치

    private bool wallSpawned = false; // 벽이 생성되었는지 여부를 나타내는 변수

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어가 충돌했고, 아직 벽이 생성되지 않았다면
        if (!wallSpawned && other.CompareTag("PLAYER"))
        {
            // 벽을 생성하고 위치를 설정
            GameObject newWall = Instantiate(wallPrefab, wallSpawnZone.position, wallSpawnZone.rotation);

            // 벽이 생성되었음을 표시
            wallSpawned = true;
        }
    }
}
