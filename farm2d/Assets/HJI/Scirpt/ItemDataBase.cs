using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
[System.Serializable]
public enum ItemType
{   Main,
    Building,
    Seed,
    Field
}

[System.Serializable]
public class Item 
{
    public int id;    
    public string itemName;
    public string imformation;
    public Sprite itemImage;
    public int value; // 아이템 가치
    public ItemType itemType;
    public int count;
    public bool Use()
    {
        return false;
    }
}
public class ItemDataBase : MonoBehaviour
{
    public static ItemDataBase instance;
    public List<Item> itemDB = new List<Item>();
    public Vector3[] pos;
    public ItemType currentType;

    public List<Item> seedItems;
    public List<Item> building;
    public List<Item> field;

    public void Awake()
    {
        currentType = ItemType.Main;
    }
    public void AddItemToDB(int id, string itemName,Sprite itemimage, string imformation, int value,ItemType itemType , int count)
    {
        if (itemDB.Exists(item => item.itemName == itemName ))
        {
            Debug.Log($"ID {itemName}의 아이템은 이미 존재한당께.");
            return;
        }
        Item newItem = new Item
        {
            id = id,
            itemName = itemName,
            itemImage = itemimage,
            imformation = imformation,
            value = value,
            itemType = itemType,
            count = count
        };

        itemDB.Add(newItem);
       
    }

    // 특정 아이템을 제거하는 메서드
    public void RemoveItemFromDB(int id)
    {
        Item itemToRemove = itemDB.Find(item => item.id == id);

        if (itemToRemove != null)
        {
            itemDB.Remove(itemToRemove);
        }
    }

    // Unity에서 호출하는 시작 메서드
    void Start()
    {
        instance = this;
        // 예시: 시작 시 아이템 몇 개를 데이터베이스에 추가
        AddList();

    }
    public Item GetItemByID(int id)
    {
        switch (currentType)
        {
            case ItemType.Seed:
                return seedItems.Find(seedItems => seedItems.id == id);
            case ItemType.Building:
                return building.Find(building => building.id == id);
                
            case ItemType.Field:
                return field.Find(field => field.id == id);
            default:
                return null;
        }

    }
    public void AddList()
    {
        
            //itemDB.Clear();
            AddItemToDB(1, "Carrot", ShopScriptUI.SSU.loadedImage.Find(sprite => sprite.name== "Carrot_Icon"), "이것은 당근이라고하는게 당근.", 500, ItemType.Seed, 99);
            AddItemToDB(2, "cabbage", ShopScriptUI.SSU.loadedImage.Find(sprite => sprite.name == "Cabbage_Icon"), "양배추랑 양상추랑 헷갈리지 마유.", 800, ItemType.Seed, 99);        
            AddItemToDB(3, "Tomato", ShopScriptUI.SSU.loadedImage.Find(sprite => sprite.name == "Tomato_Icon"), "토마토는 거꾸로 해도 토마토.", 500, ItemType.Seed, 99);
            AddItemToDB(4, "Onion", ShopScriptUI.SSU.loadedImage[0], "이것은 까도까도 까이는 양파여.", 600, ItemType.Seed, 99);
            AddItemToDB(5, "Pumpkin", ShopScriptUI.SSU.loadedImage.Find(sprite => sprite.name == "Pumpkin_Icon"), "내 이름은 잭 오 랜턴. 그냥 호박이죠", 1000, ItemType.Seed, 99);
            AddItemToDB(1, "house", ShopScriptUI.SSU.loadedImage.Find(sprite => sprite.name == "House_1"), "집이지만 비닐 하우스", 2000, ItemType.Building, 2);        
            AddItemToDB(2, "storage", ShopScriptUI.SSU.loadedImage[1], "아마도 창고인가..?", 3000, ItemType.Building, 3);        
            AddItemToDB(1, "큰 땅", ShopScriptUI.SSU.loadedImage[2], "밭이 2개면 입이 20개", 2000, ItemType.Field, 10);       
            AddItemToDB(2, "작은 땅", ShopScriptUI.SSU.loadedImage.Find(sprite => sprite.name == "Floor"), "밭 싱글", 3000, ItemType.Field, 10);
           
       
    }
    private void Update()
    {
        reference();
        fitering();
    }
    public  void fitering()
    {
        if(currentType ==ItemType.Seed)
        {
            seedItems = ItemDataBase.instance.itemDB.FindAll(item => item.itemType == ItemType.Seed);
        }
        else if (currentType == ItemType.Building)
        {
            building = ItemDataBase.instance.itemDB.FindAll(item => item.itemType == ItemType.Building);
        }
        else if( currentType == ItemType.Field)
        {
            field = ItemDataBase.instance.itemDB.FindAll(item => item.itemType == ItemType.Field);
        }
        
    }
    public void reference()
    {
        if (currentType == ItemType.Seed)
        {
            for (int i = 0; i < seedItems.Count; i++)
            {   
                ShopScriptUI.SSU.slots[i].GetComponent<Image>().sprite = seedItems[i].itemImage;
            }
        }
        else if (currentType == ItemType.Building)
        {
            for (int i = 0; i < building.Count; i++)
            {
                ShopScriptUI.SSU.slots[i].GetComponent<Image>().sprite = building[i].itemImage;
            }
        }
        else if (currentType == ItemType.Field)
        {
            for (int i = 0; i < field.Count; i++)
            {
                ShopScriptUI.SSU.slots[i].GetComponent<Image>().sprite = field[i].itemImage;
            }
        }
        else
        {
            for (int i = 0; i < ShopScriptUI.SSU.slots.Length; i++)
            {
                ShopScriptUI.SSU.slots[i].GetComponent<Image>().sprite = null;
            }
        }
    }

}

