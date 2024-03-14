using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Inventory")]
public class InventoryManager : ScriptableObject
{
    public List<InvenPlant> items = new List<InvenPlant>();

    // 아이템 추가 메서드
    public void AddItem(InvenPlant item)
    {
        items.Add(item);
    }

    // 아이템 제거 메서드
    public void RemoveItem(InvenPlant item)
    {
        items.Remove(item);
    }
}