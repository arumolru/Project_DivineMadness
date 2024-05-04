using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeypadScript : MonoBehaviour
{
    public Text[] numberTexts; // 상단의 번호를 표시할 Text 배열
    public int[] correctCode = { 1, 4, 9, 20, 22 }; // 정답 비밀번호 배열
    public Button[] numberButtons; // 버튼들의 배열

    public GameObject keypadUI; // UI Image를 담을 GameObject
    public GameObject keypadDoor; // KeypadDoor 게임 오브젝트

    private List<int> enteredCode; // 사용자가 입력한 비밀번호 리스트
    private bool isKeypadActive = false; // Keypad UI가 활성화되었는지 여부
    private bool isDoorOpen = false; // 문이 열렸는지 여부

    private void Awake()
    {
        enteredCode = new List<int>();
        keypadUI.SetActive(false); // 시작할 때는 UI Image가 비활성화된 상태
    }

    private void Update()
    {
        // Keypad UI가 활성화되었을 때 ESC 키를 누르면 UI를 비활성화하고 게임 시간을 다시 진행시킴
        if (isKeypadActive && Input.GetKeyDown(KeyCode.Escape))
        {
            DeactivateKeypadUI();
        }
    }

    public void ShowKeypadUI()
    {
        // Keypad UI를 활성화하여 보여줌
        keypadUI.SetActive(true);
        isKeypadActive = true;
        Time.timeScale = 0f; // 게임 시간을 멈춤
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        ClearEnteredCode(); // 사용자가 입력한 비밀번호 초기화
    }

    public void OnNumberButtonClick(int number)
    {
        // 숫자 버튼이 클릭되었을 때 호출되는 메서드
        if (enteredCode.Count < numberTexts.Length)
        {
            enteredCode.Add(number); // 사용자가 입력한 비밀번호에 숫자 추가
            UpdateNumberTexts(); // 상단의 번호 표시 업데이트
        }

        if (enteredCode.Count >= numberTexts.Length)
        {
            CheckCode(); // 비밀번호 확인
        }
    }

    private void UpdateNumberTexts()
    {
        // 상단의 번호 텍스트 업데이트
        for (int i = 0; i < enteredCode.Count; i++)
        {
            numberTexts[i].text = enteredCode[i].ToString();
        }
    }

    private void CheckCode()
    {
        // 비밀번호 확인
        bool isCodeCorrect = true;

        for (int i = 0; i < correctCode.Length; i++)
        {
            if (enteredCode[i] != correctCode[i])
            {
                isCodeCorrect = false;
                break;
            }
        }

        if (isCodeCorrect && !isDoorOpen)
        {
            Debug.Log("비밀번호가 정확합니다. 잠금이 해제됩니다.");

            // KeypadDoor를 열도록 DoorScript 컴포넌트에 접근하여 OpenDoor 메서드 호출
            KeypadDoorScript keypaddoorScript = keypadDoor.GetComponent<KeypadDoorScript>();
            if (keypaddoorScript != null)
            {
                keypaddoorScript.OpenDoor2();
            }

            isDoorOpen = true; // 문이 열렸음을 표시

            DeactivateKeypadUI(); // UI 창 비활성화
        }
        else
        {
            Debug.Log("비밀번호가 틀렸습니다. 다시 입력해주세요.");
            ClearEnteredCode(); // 사용자가 입력한 비밀번호 초기화
            ClearNumberTexts(); // 상단의 번호 텍스트 초기화
        }
    }

    private void ClearEnteredCode()
    {
        // 사용자가 입력한 비밀번호 초기화
        enteredCode.Clear();
    }

    private void ClearNumberTexts()
    {
        // 상단의 번호 텍스트 초기화
        for (int i = 0; i < numberTexts.Length; i++)
        {
            numberTexts[i].text = "";
        }
    }

    private void DeactivateKeypadUI()
    {
        // Keypad UI를 비활성화하고 게임 시간을 다시 진행시킴
        keypadUI.SetActive(false);
        isKeypadActive = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f; // 게임 시간을 다시 진행시킴
    }

    // Close 버튼 클릭 이벤트 핸들러
    public void CloseButtonClicked()
    {
        DeactivateKeypadUI();
    }
}
