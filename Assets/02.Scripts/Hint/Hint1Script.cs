using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hint1Script : MonoBehaviour
{
    public GameObject hintUI; // UI Image를 담을 GameObject

    private bool isHintShown; // 힌트가 보여지고 있는지 여부

    private void Start()
    {
        isHintShown = false;
        hintUI.SetActive(false); // 시작할 때는 UI Image가 비활성화된 상태
    }

    private void Update()
    {
        if (isHintShown)
        {
            // UI가 활성화된 경우

            // ESC 키를 누르거나 Close 버튼을 클릭하면 UI를 종료
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CloseButtonClicked();
            }
        }
    }

    public void ShowHintUI()
    {
        isHintShown = true; // 힌트를 보여주는 상태로 변경
        hintUI.SetActive(true); // UI Image를 활성화하여 보여줌
        Time.timeScale = 0f; // 게임 시간을 멈춤
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void HideHintUI()
    {
        isHintShown = false; // 힌트가 보여지는 상태를 해제
        hintUI.SetActive(false); // UI Image를 비활성화하여 숨김
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f; // 게임 시간을 다시 시작
    }

    // Close 버튼 클릭 이벤트 핸들러
    public void CloseButtonClicked()
    {
        HideHintUI();
    }
}
