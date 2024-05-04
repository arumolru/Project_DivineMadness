using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    public List<GameObject> inventory = new List<GameObject>(); // 인벤토리 아이템 리스트

    // 인벤토리에 아이템 추가
    public void AddItem(GameObject item)
    {
        inventory.Add(item);
    }

    // 인벤토리에서 아이템 제거
    public void RemoveItem(GameObject item)
    {
        inventory.Remove(item);
    }

    // 인벤토리 아이템 리스트 반환
    public List<GameObject> GetInventory()
    {
        return inventory;
    }
}