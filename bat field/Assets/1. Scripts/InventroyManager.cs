using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventroyManager : MonoBehaviour
{
    public GameObject inventorySlotPrefab;
    public Transform inventoryParent;

    private List<GameObject> inventorySlots = new List<GameObject>();

    void Start()
    {
        // 인벤토리 칸 생성
        CreateInventorySlots();
    }

    void CreateInventorySlots()
    {
        for (int i = 0; i < 64; i++)
        {
            // 인벤토리 칸 생성
            GameObject slot = Instantiate(inventorySlotPrefab, inventoryParent);
            // 인벤토리 칸을 리스트에 추가
            inventorySlots.Add(slot);
        }
    }
}
