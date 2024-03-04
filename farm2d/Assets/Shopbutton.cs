using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Shopbutton : MonoBehaviour
{
    public int currentId;
    public List<Item> seedItems;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void DisplayItemInfo(Item item)
    {
        
                GameManager.GM.slotInfo[1].GetComponent<TextMeshProUGUI>().text = item.itemName;
                GameManager.GM.slotInfo[2].GetComponent<TextMeshProUGUI>().text = item.imformation;
                GameManager.GM.slotInfo[3].GetComponent<TextMeshProUGUI>().text = item.value.ToString() + " gold";
           
        
    }
    public void OnButtonClick(int i)
    {
        // ���⼭ ������ ������ �������� ������ �����ϰ�
        // ������ ������ ������ DisplayItemInfo �޼��带 ���� UI�� ǥ��
        Item item = ItemDataBase.instance.GetItemByID(i);
        currentId = i;
        if (item != null)
        {
            DisplayItemInfo(item);
        }
        else
        {
            return;
        }

    }
    public void OnBuyButtonClick()
    {
        Item item = ItemDataBase.instance.GetItemByID(currentId);

        if (item != null)
        {
            item.count--;
            buy(item);
        }
        else
        {
            Debug.LogWarning("Item with ID " + currentId + " not found.");
        }
    }
    public void buy(Item item)
    {
        if (ShopScriptUI.SSU.gold >= item.value)
        {
            // ���� ������ ���
            ShopScriptUI.SSU.gold -= item.value; // ��� ����
                                               // ���⿡�� �������� �÷��̾� �κ��丮�� �߰��ϴ� ���� �۾� ����
                                               // (������ �߰� ����� ������Ʈ�� ���� �ٸ� �� �ֽ��ϴ�)
            Debug.Log("Item purchased: " + item.itemName);
        }
        else
        {
            // ��尡 ������ ���
            Debug.Log("Not enough gold to buy: " + item.itemName);
        }
    }
    public void ClickType_Building()
    {
        ClickType(ItemType.Building);
        for(int i =0; i<ShopScriptUI.SSU.slots.Length;i++)
        {
            ShopScriptUI.SSU.slots[i].GetComponent<Image>().sprite = null;
        }
    }

    public void ClickType_Seed()
    {
        ClickType(ItemType.Seed);
        for (int i = 0; i < ShopScriptUI.SSU.slots.Length; i++)
        {
            ShopScriptUI.SSU.slots[i].GetComponent<Image>().sprite = null;
        }
    }
    public void ClickType_Field()
    {
        ClickType(ItemType.Field);
        for (int i = 0; i < ShopScriptUI.SSU.slots.Length; i++)
        {
            ShopScriptUI.SSU.slots[i].GetComponent<Image>().sprite = null;
        }
    }
    public void ClickType(ItemType itemType)
    {
        {
            ItemDataBase.instance.currentType = itemType;
        }
    }
}