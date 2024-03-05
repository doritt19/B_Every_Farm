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
    public int value; // ������ ��ġ
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
            Debug.Log($"ID {itemName}�� �������� �̹� �����Ѵ粲.");
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

    // Ư�� �������� �����ϴ� �޼���
    public void RemoveItemFromDB(int id)
    {
        Item itemToRemove = itemDB.Find(item => item.id == id);

        if (itemToRemove != null)
        {
            itemDB.Remove(itemToRemove);
        }
    }

    // Unity���� ȣ���ϴ� ���� �޼���
    void Start()
    {
        instance = this;
        // ����: ���� �� ������ �� ���� �����ͺ��̽��� �߰�
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
            AddItemToDB(1, "Carrot", ShopScriptUI.SSU.loadedImage.Find(sprite => sprite.name== "Carrot_Icon"), "�̰��� ����̶���ϴ°� ���.", 500, ItemType.Seed, 99);
            AddItemToDB(2, "cabbage", ShopScriptUI.SSU.loadedImage.Find(sprite => sprite.name == "Cabbage_Icon"), "����߶� ����߶� �򰥸��� ����.", 800, ItemType.Seed, 99);        
            AddItemToDB(3, "Tomato", ShopScriptUI.SSU.loadedImage.Find(sprite => sprite.name == "Tomato_Icon"), "�丶��� �Ųٷ� �ص� �丶��.", 500, ItemType.Seed, 99);
            AddItemToDB(4, "Onion", ShopScriptUI.SSU.loadedImage[0], "�̰��� �� ���̴� ���Ŀ�.", 600, ItemType.Seed, 99);
            AddItemToDB(5, "Pumpkin", ShopScriptUI.SSU.loadedImage.Find(sprite => sprite.name == "Pumpkin_Icon"), "�� �̸��� �� �� ����. �׳� ȣ������", 1000, ItemType.Seed, 99);
            AddItemToDB(1, "house", ShopScriptUI.SSU.loadedImage.Find(sprite => sprite.name == "House_1"), "�������� ��� �Ͽ콺", 2000, ItemType.Building, 2);        
            AddItemToDB(2, "storage", ShopScriptUI.SSU.loadedImage[1], "�Ƹ��� â���ΰ�..?", 3000, ItemType.Building, 3);        
            AddItemToDB(1, "ū ��", ShopScriptUI.SSU.loadedImage[2], "���� 2���� ���� 20��", 2000, ItemType.Field, 10);       
            AddItemToDB(2, "���� ��", ShopScriptUI.SSU.loadedImage.Find(sprite => sprite.name == "Floor"), "�� �̱�", 3000, ItemType.Field, 10);
           
       
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

