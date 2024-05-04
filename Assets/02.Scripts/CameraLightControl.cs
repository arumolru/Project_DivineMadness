using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLightControl : MonoBehaviour
{
    private Light mainLight;
    public GameObject gameOverMonsterPrefab; // 게임 오버 몬스터 프리팹 연결
    public Transform gameOverMonsterSpawnPoint; // GameOverMonsterSpawn의 위치를 연결
    public AudioSource soundEffect; // AudioSource 컴포넌트 연결

    void Start()
    {
        mainLight = GetComponent<Light>();
        mainLight.enabled = false; // 처음에 비활성화

        StartCoroutine(ActivateLightAfterDelay());
    }

    IEnumerator ActivateLightAfterDelay()
    {
        yield return new WaitForSeconds(1.5f); // 1.5초 대기
        mainLight.enabled = true; // 1..5초 뒤에 라이트 활성화

        yield return new WaitForSeconds(2.0f); // 2초 대기
        mainLight.enabled = false; // 2초 뒤에 라이트 비활성화

        // GameOverMonsterSpawn 오브젝트 위치에서 GameOverMonster를 생성
        GameObject gameOverMonster = Instantiate(gameOverMonsterPrefab, gameOverMonsterSpawnPoint.position, Quaternion.identity);

        yield return new WaitForSeconds(3.7f); // 4초 대기
        mainLight.enabled = true; // 2초 뒤에 라이트 활성화

        yield return new WaitForSeconds(1.0f); // 1.0초 대기
        mainLight.enabled = false; // 1.0초 뒤에 라이트 비활성화

        // 사운드 재생
        soundEffect.Play();
    }
}
