using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageClearScript : MonoBehaviour
{
    public GameObject clearUI; // Hierarchy에 직접 있는 클리어 UI를 여기에 연결
    public string nextSceneName = "NextSceneName"; // 다음 씬 이름 설정

    private void Start()
    {
        // 게임 시작 시 클리어 UI를 비활성화
        clearUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 게임 시간을 멈추기
            Time.timeScale = 0f;

            // 클리어 UI 활성화
            clearUI.SetActive(true);

            // 마우스 커서 잠금 해제
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // "NEXT STAGE" 버튼에 클릭 이벤트 추가
            Button nextStageButton = clearUI.GetComponentInChildren<Button>();
            if (nextStageButton != null)
            {
                nextStageButton.onClick.AddListener(LoadNextScene);
            }
        }
    }

    private void LoadNextScene()
    {
        // 클리어 UI 비활성화
        clearUI.SetActive(false);

        // 다음 씬으로 이동
        SceneManager.LoadScene(nextSceneName);

        // 마우스 커서 잠금 설정
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // 게임 시간 다시 시작
        Time.timeScale = 1f;
    }
}
