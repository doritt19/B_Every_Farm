using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<InvenPlant> plants;
    public InventoryManager inventoryManager;

    [SerializeField] private Transform invenSlotParent;
    [SerializeField] private InvenSlot[] invenSlots;


    private void OnValidate()
    {
        invenSlots = invenSlotParent.GetComponentsInChildren<InvenSlot>();
    }
    void Awake()
    {
        FreshSlot();
    }

    public void FreshSlot()
    {
        int i = 0;
        for (; i < plants.Count && i < invenSlots.Length; i++)
        {
            invenSlots[i].plant = plants[i];
        }
        for (; i < invenSlots.Length; i++)
        {
            invenSlots[i].plant = null;
        }
    }

    public void AddItem(InvenPlant _plant)
    {
        if (plants.Count < invenSlots.Length)
        {
            plants.Add(_plant);
            inventoryManager.AddItem(_plant);
            FreshSlot();
            Debug.Log("슬롯에 아이템 넣기");
        }
        else
        {
            Debug.Log("슬롯이 가득 차 있습니다.");
        }
    }
}