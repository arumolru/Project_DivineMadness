using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PictureScript : MonoBehaviour
{
    public GameObject pictureUI; // UI Image를 담을 GameObject
    public Sprite[] pictures; // 사진 배열
    public int currentPictureIndex; // 현재 보여지고 있는 사진의 인덱스

    private bool isPictureShown; // 현재 사진이 보여지고 있는지 여부

    private void Start()
    {
        // 초기화
        currentPictureIndex = 0;
        isPictureShown = false;
        pictureUI.SetActive(false); // 시작할 때는 UI Image가 비활성화된 상태
    }

    private void Update()
    {
        if (isPictureShown)
        {
            // UI가 활성화된 경우

            // ESC 키를 누르거나 Close 버튼을 클릭하면 UI를 종료
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CloseButtonClicked();
            }

            // D 키를 누르면 다음 이미지로 전환
            /*
            if (Input.GetKeyDown(KeyCode.E))
            {
                NextButtonClicked();
            }

            // A 키를 누르면 이전 이미지로 전환
            if (Input.GetKeyDown(KeyCode.Q))
            {
                PreviousButtonClicked();
            }
            */
        }
    }

    public void ShowPictureUI()
    {
        isPictureShown = true; // 사진을 보여주는 상태로 변경
        pictureUI.SetActive(true); // UI Image를 활성화하여 보여줌
        pictureUI.GetComponent<Image>().sprite = pictures[currentPictureIndex]; // 현재 사진을 UI Image에 할당
        Time.timeScale = 0f; // 게임 시간을 멈춤
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void HidePictureUI()
    {
        isPictureShown = false; // 사진이 보여지는 상태를 해제
        pictureUI.SetActive(false); // UI Image를 비활성화하여 숨김
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f; // 게임 시간을 다시 시작
    }

    public void ShowNextPicture()
    {
        if (currentPictureIndex < pictures.Length - 1) // 다음 사진이 존재할 때
        {
            currentPictureIndex++; // 다음 사진의 인덱스로 변경
            pictureUI.GetComponent<Image>().sprite = pictures[currentPictureIndex]; // UI Image에 다음 사진을 할당
        }
    }

    public void ShowPreviousPicture()
    {
        if (currentPictureIndex > 0) // 이전 사진이 존재할 때
        {
            currentPictureIndex--; // 이전 사진의 인덱스로 변경
            pictureUI.GetComponent<Image>().sprite = pictures[currentPictureIndex]; // UI Image에 이전 사진을 할당
        }
    }

    // Close 버튼 클릭 이벤트 핸들러
    public void CloseButtonClicked()
    {
        HidePictureUI();
    }

    // Next 버튼 클릭 이벤트 핸들러
    public void NextButtonClicked()
    {
        ShowNextPicture();
    }

    // Previous 버튼 클릭 이벤트 핸들러
    public void PreviousButtonClicked()
    {
        ShowPreviousPicture();
    }
}

