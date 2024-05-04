using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastScript : MonoBehaviour
{
    public float raycastDistance = 20f; // Raycast의 최대 거리

    private bool hasKey = false; // 열쇠를 가지고 있는지 여부
    private bool isDoorOpened = false; // 문이 열려 있는지 여부
    private bool isVentOpened = false;
    private bool isDoor1Opened = false;

    void Update()
    {
        // E 키를 누르면 Raycast 실행
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Raycast 실행
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, raycastDistance))
            {
                // 충돌한 오브젝트의 태그가 "Key"인 경우
                if (hit.collider.CompareTag("Key"))
                {
                    // 열쇠를 획득한 경우
                    hasKey = true;
                    Destroy(hit.collider.gameObject); // 열쇠 오브젝트 제거
                    Debug.Log("열쇠를 얻습니다.");
                }
                else if (hit.collider.CompareTag("Vent") && !isVentOpened) // 정상 작동 Vent 열기
                {
                    isVentOpened = true;
                    hit.collider.gameObject.GetComponent<VentScript>().OpenVent();
                    Debug.Log("Vent를 엽니다.");
                }
                else if (hit.collider.CompareTag("Box"))
                {
                    Destroy(hit.collider.gameObject);
                    Debug.Log("상자를 부숩니다.");
                }
                else if (hit.collider.CompareTag("Door1") && !isDoor1Opened) 
                {
                    isDoor1Opened = true;
                    hit.collider.gameObject.GetComponent<Door1Script>().OpenDoor1();
                    Debug.Log("문을 엽니다.");
                }
                else if (hit.collider.CompareTag("Door") && !isDoorOpened && hasKey)
                {
                    // 열쇠를 가지고 있고, 문이 열려 있지 않은 경우
                    isDoorOpened = true;
                    hit.collider.gameObject.GetComponent<DoorScript>().OpenDoor(); // DoorScript의 OpenDoor() 메서드 호출
                    Debug.Log("문을 엽니다.");
                }
                else if (hit.collider.CompareTag("Calendar"))
                {
                    // Calendar 태그인 경우
                    Debug.Log("달력과 상호 작용합니다.");

                    // PictureScript를 실행하기 위해 PictureScript 컴포넌트를 찾음
                    PictureScript pictureScript = hit.collider.GetComponent<PictureScript>();
                    if (pictureScript != null)
                    {
                        // PictureScript 실행
                        pictureScript.ShowPictureUI();
                    }
                }
                else if (hit.collider.CompareTag("HintCalendar3"))
                {
                    Debug.Log("힌트와 상호 작용합니다.");
                    HintScript hintScript = hit.collider.GetComponent<HintScript>();
                    if (hintScript != null)
                    {
                        hintScript.ShowHintUI();
                    }
                }
                else if (hit.collider.CompareTag("HintCalendar7"))
                {
                    Debug.Log("힌트와 상호 작용합니다.");
                    Hint1Script hint1Script = hit.collider.GetComponent<Hint1Script>();
                    if (hint1Script != null)
                    {
                        hint1Script.ShowHintUI();
                    }
                }
                else if (hit.collider.CompareTag("HintBible"))
                {
                    Debug.Log("힌트와 상호 작용합니다.");
                    Hint2Script hint2Script = hit.collider.GetComponent<Hint2Script>();
                    if (hint2Script != null)
                    {
                        hint2Script.ShowHintUI();
                    }
                }
                else if (hit.collider.CompareTag("Keypad"))
                {
                    // Keypad 태그인 경우
                    Debug.Log("키패드와 상호 작용합니다.");

                    // KeypadScript를 실행하기 위해 KeypadScript 컴포넌트를 찾음
                    KeypadScript keypadScript = hit.collider.GetComponent<KeypadScript>();
                    if (keypadScript != null)
                    {
                        // KeypadScript 실행
                        keypadScript.ShowKeypadUI();
                    }
                }
                else if (!hasKey)
                {
                    // 열쇠를 가지고 있지 않은 경우
                    Debug.Log("열쇠가 없습니다.");
                }
            }
        }
    }
}