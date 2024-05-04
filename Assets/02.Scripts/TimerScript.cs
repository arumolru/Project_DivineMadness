using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{
    public float timeLimit = 120.0f; // 타이머 제한 시간 (2분)
    public Text timerText; // 타이머를 표시할 UI 텍스트
    public string gameoverSceneName = "GameoverScene"; // Game Over 시 이동할 Scene의 이름

    private bool isTimerStarted = false; // 타이머가 시작되었는지 여부
    private float currentTime = 0.0f; // 현재 남은 시간

    private Coroutine timerCoroutine; // 타이머 코루틴

    // 특정 구역 진입 시 타이머 시작
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTimerStarted = true;
            currentTime = timeLimit;
            timerText.gameObject.SetActive(true);

            if (timerCoroutine != null)
                StopCoroutine(timerCoroutine);

            timerCoroutine = StartCoroutine(UpdateTimer());
        }
    }

    // 타이머 업데이트 코루틴
    private IEnumerator UpdateTimer()
    {
        while (currentTime > 0.0f)
        {
            currentTime -= 1.0f;
            timerText.text = Mathf.FloorToInt(currentTime / 60).ToString("00") + " : " + Mathf.FloorToInt(currentTime % 60).ToString("00");
            yield return new WaitForSeconds(1.0f);
        }

        GameOver();
    }

    // 게임 오버 처리
    private void GameOver()
    {
        timerText.text = "Game Over";
        isTimerStarted = false;
        
        SceneManager.LoadScene(gameoverSceneName); // Game Over 시 Scene 변경
    }

    // 플레이어가 구역을 빠져나갈 경우 타이머 중지
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isTimerStarted)
            {
                if (timerCoroutine != null)
                    StopCoroutine(timerCoroutine);

                timerText.gameObject.SetActive(false);
                isTimerStarted = false;
            }
        }
    }
}
