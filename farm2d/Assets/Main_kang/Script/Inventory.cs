using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Inventory : MonoBehaviour
{
    public List<InvenPlant> plants;
    public InventoryManager inventoryManager;
    public bool inventoryFull; // 인벤토리가 꽉차있으면 수확이 불가능하다를 판단

    public GameObject allSellPanel; // 모든 판매 패널
    public Text allSellText; // 판매 스킬의 텍스트를 담을 공간

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
            // ScriptableObject에 있는 아이템을 불러와서 인벤토리에 추가하는 로직을 여기에 작성합니다.
            foreach (InvenPlant item in inventoryManager.items)
            {
                plants.Add(item);
                sellAllGold += item.plantGold;
                // 인벤토리에 아이템 추가하는 코드
                Debug.Log("인벤토리 아이템 로드: " + item.name);
            }
            allSellText.text = " 모든 수확물을 판매하시겠습니까?\r\n+" + sellAllGold.ToString() + "원";
            if (sellAllGold == 0)
            {
                allSellText.text = "판매가능한 수확물이 없습니다!";
            }
           
        }
        else
        {
            Debug.LogWarning("인벤토리 데이터가 설정되지 않았습니다!");
        }
    }
    public void SellAll()
    {
        if (plants != null) // 인벤토리에 아이템이 있는 경우에만 처리합니다.
        {
            List<InvenPlant> plantsToRemove = new List<InvenPlant>(plants); // 인벤토리 아이템의 복제본을 만듭니다.
            foreach (InvenPlant plant in plantsToRemove) // 복제본을 통해 반복합니다.
            {
                RemoveItem(plant);
            }
        }
        allSellText.text = "판매가능한 수확물이 없습니다!";
        allSellPanel.SetActive(false); // 판매 패널을 비활성화합니다.
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
        allSellText.text = " 모든 수확물을 판매하시겠습니까?\r\n+" + sellAllGold.ToString() + "원";

        if (sellAllGold == 0)
        {
            allSellText.text = "판매가능한 수확물이 없습니다!";
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
            inventoryFull = false;
            if(plants.Count == invenSlots.Length)
            {
                inventoryFull = true;
            }
        }
        else
        {
            inventoryFull = true;
            Debug.Log("슬롯이 가득 차 있습니다.");
            
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
            Debug.Log("아이템을 인벤토리에서 제거했습니다.");
            
        }
        else
        {
            Debug.Log("인벤토리에 해당 아이템이 없습니다.");
        }
    }
}