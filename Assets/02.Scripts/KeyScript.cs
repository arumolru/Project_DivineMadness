using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public string keyName; // 열쇠 이름
    public GameObject door; // 해당 열쇠로 열 수 있는 문 오브젝트
    public InventoryScript inventoryScript; // 인벤토리 스크립트

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 플레이어가 열쇠를 습득할 경우
            inventoryScript.AddItem(gameObject); // 인벤토리에 열쇠 아이템 추가
            gameObject.SetActive(false); // 열쇠 아이템 비활성화
        }
    }

    // 열쇠 사용 시
    public void UseKey()
    {
        if (inventoryScript != null && door != null)
        {
            inventoryScript.RemoveItem(gameObject); // 인벤토리에서 열쇠 아이템 제거
            door.GetComponent<DoorScript>().OpenDoor(); // 해당 문 열기
        }
        else
        {
            Debug.Log("인벤토리 스크립트 또는 문이 설정되지 않았습니다.");
        }
}
}