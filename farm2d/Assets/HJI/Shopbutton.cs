using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Shopbutton : MonoBehaviour
{
    public int[] vagetableSeed; //2024-03-13 �κ�-���� ���� �߰�



    public int currentId;
    public List<Item> seedItems;
    public Text[] seedText;

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

        SlotManager.GM.slotInfo[1].GetComponent<TextMeshProUGUI>().text = item.itemName;
        SlotManager.GM.slotInfo[2].GetComponent<TextMeshProUGUI>().text = item.imformation;
        SlotManager.GM.slotInfo[4].GetComponent<TextMeshProUGUI>().text = item.value.ToString();



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
        if (ShopScriptUI.gold >= item.value)
        {
            // ���� ������ ���
            ShopScriptUI.gold -= item.value; // ��� ����
            //2024-03-13 �κ�-���� ���� �߰�
            Item items = ItemDataBase.instance.GetItemByID(currentId);   // ���⿡�� �������� �÷��̾� �κ��丮�� �߰��ϴ� ���� �۾� ����
            switch (items.id)
            {
                case 1:
                    vagetableSeed[0] += 1; // ���
                    seedText[0].text = vagetableSeed[0].ToString();
                    break;
                case 2:
                    vagetableSeed[1] += 1; // �����
                    seedText[1].text = vagetableSeed[1].ToString();
                    break;
                case 5:
                    vagetableSeed[2] += 1; // ȣ��
                    seedText[2].text = vagetableSeed[2].ToString();
                    break;
                case 3:
                    vagetableSeed[3] += 1; // �丶��
                    seedText[3].text = vagetableSeed[3].ToString();
                    break;


            }     // (������ �߰� ����� ������Ʈ�� ���� �ٸ� �� �ֽ��ϴ�)
            Debug.Log("Item purchased: " + item.itemName + "a " + item.imformation);
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
        for (int i = 0; i < ShopScriptUI.SSU.slots.Length; i++)
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
