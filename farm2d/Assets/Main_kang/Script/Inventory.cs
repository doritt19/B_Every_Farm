using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Inventory : MonoBehaviour
{
    public List<InvenPlant> plants;
    public InventoryManager inventoryManager;
    public bool inventoryFull; // �κ��丮�� ���������� ��Ȯ�� �Ұ����ϴٸ� �Ǵ�

    public GameObject allSellPanel; // ��� �Ǹ� �г�
    public Text allSellText; // �Ǹ� ��ų�� �ؽ�Ʈ�� ���� ����

    [SerializeField] private Transform invenSlotParent;
    [SerializeField] private InvenSlot[] invenSlots;
    [SerializeField] int sellAllGold;


    private void OnValidate()
    {
        invenSlots = invenSlotParent.GetComponentsInChildren<InvenSlot>();
       
    }
    void Awake()
    {
        LoadInventory();
        FreshSlot();
    }
    public void LoadInventory()
    {
        sellAllGold = 0;
        if (inventoryManager != null)
        {
            // ScriptableObject�� �ִ� �������� �ҷ��ͼ� �κ��丮�� �߰��ϴ� ������ ���⿡ �ۼ��մϴ�.
            foreach (InvenPlant item in inventoryManager.items)
            {
                plants.Add(item);
                sellAllGold += item.plantGold;
                // �κ��丮�� ������ �߰��ϴ� �ڵ�
                Debug.Log("�κ��丮 ������ �ε�: " + item.name);
            }
            allSellText.text = " ��� ��Ȯ���� �Ǹ��Ͻðڽ��ϱ�?\r\n+" + sellAllGold.ToString() + "��";
            if (sellAllGold == 0)
            {
                allSellText.text = "�ǸŰ����� ��Ȯ���� �����ϴ�!";
            }
           
        }
        else
        {
            Debug.LogWarning("�κ��丮 �����Ͱ� �������� �ʾҽ��ϴ�!");
        }
    }
    public void SellAll()
    {
        if (plants != null) // �κ��丮�� �������� �ִ� ��쿡�� ó���մϴ�.
        {
            List<InvenPlant> plantsToRemove = new List<InvenPlant>(plants); // �κ��丮 �������� �������� ����ϴ�.
            foreach (InvenPlant plant in plantsToRemove) // �������� ���� �ݺ��մϴ�.
            {
                RemoveItem(plant);
            }
        }
        allSellText.text = "�ǸŰ����� ��Ȯ���� �����ϴ�!";
        allSellPanel.SetActive(false); // �Ǹ� �г��� ��Ȱ��ȭ�մϴ�.
    }
    public void FreshSlot()
    {
        int i = 0;
        sellAllGold = 0;
        for (; i < plants.Count && i < invenSlots.Length; i++)
        {
            plants[i].invenNum = i;
            invenSlots[i].plant = plants[i];
            sellAllGold += plants[i].plantGold;

        }
        for (; i < invenSlots.Length; i++)
        {
            invenSlots[i].plant = null;
            inventoryFull = false;
        }
        allSellText.text = " ��� ��Ȯ���� �Ǹ��Ͻðڽ��ϱ�?\r\n+" + sellAllGold.ToString() + "��";

        if (sellAllGold == 0)
        {
            allSellText.text = "�ǸŰ����� ��Ȯ���� �����ϴ�!";
        }
    }
    
    public void AddItem(InvenPlant _plant)
    {
        if (plants.Count < invenSlots.Length)
        {
            plants.Add(_plant);
            inventoryManager.AddItem(_plant);
            FreshSlot();

            Debug.Log("���Կ� ������ �ֱ�");
            inventoryFull = false;
            if(plants.Count == invenSlots.Length)
            {
                inventoryFull = true;
            }
        }
        else
        {
            inventoryFull = true;
            Debug.Log("������ ���� �� �ֽ��ϴ�.");
            
        }
    }
    public void RemoveItem(InvenPlant _plant)
    {
        if (plants.Contains(_plant))
        {
            plants.Remove(_plant);
            inventoryManager.RemoveItem(_plant);
            PlayerPrefs.SetInt(GameManager.goldCountKey, PlayerPrefs.GetInt(GameManager.goldCountKey) + _plant.plantGold);
            PlayerPrefs.Save();
            FreshSlot();
            Debug.Log("�������� �κ��丮���� �����߽��ϴ�.");
            
        }
        else
        {
            Debug.Log("�κ��丮�� �ش� �������� �����ϴ�.");
        }
    }
}