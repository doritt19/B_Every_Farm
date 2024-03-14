using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Inventory")]
public class InventoryManager : ScriptableObject
{
    public List<InvenPlant> items = new List<InvenPlant>();

    // ������ �߰� �޼���
    public void AddItem(InvenPlant item)
    {
        items.Add(item);
    }

    // ������ ���� �޼���
    public void RemoveItem(InvenPlant item)
    {
        items.Remove(item);
    }
}