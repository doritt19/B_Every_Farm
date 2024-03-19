using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Shopbutton : MonoBehaviour
{
    public int[] vagetableSeed; //2024-03-13 인벤-상점 연동 추가



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
        // 여기서 아이템 정보를 가져오는 로직을 구현하고
        // 가져온 아이템 정보를 DisplayItemInfo 메서드를 통해 UI에 표시
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
            // 구매 가능한 경우
            ShopScriptUI.gold -= item.value; // 골드 차감
            //2024-03-13 인벤-상점 연동 추가
            Item items = ItemDataBase.instance.GetItemByID(currentId);   // 여기에서 아이템을 플레이어 인벤토리에 추가하는 등의 작업 수행
            switch (items.id)
            {
                case 1:
                    vagetableSeed[0] += 1; // 당근
                    seedText[0].text = vagetableSeed[0].ToString();
                    break;
                case 2:
                    vagetableSeed[1] += 1; // 양배추
                    seedText[1].text = vagetableSeed[1].ToString();
                    break;
                case 5:
                    vagetableSeed[2] += 1; // 호박
                    seedText[2].text = vagetableSeed[2].ToString();
                    break;
                case 3:
                    vagetableSeed[3] += 1; // 토마토
                    seedText[3].text = vagetableSeed[3].ToString();
                    break;


            }     // (아이템 추가 방식은 프로젝트에 따라 다를 수 있습니다)
            Debug.Log("Item purchased: " + item.itemName + "a " + item.imformation);
        }
        else
        {
            // 골드가 부족한 경우
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
