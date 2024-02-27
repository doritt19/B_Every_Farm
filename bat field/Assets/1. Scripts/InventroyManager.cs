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
        // �κ��丮 ĭ ����
        CreateInventorySlots();
    }

    void CreateInventorySlots()
    {
        for (int i = 0; i < 64; i++)
        {
            // �κ��丮 ĭ ����
            GameObject slot = Instantiate(inventorySlotPrefab, inventoryParent);
            // �κ��丮 ĭ�� ����Ʈ�� �߰�
            inventorySlots.Add(slot);
        }
    }
}
