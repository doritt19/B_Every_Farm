using System.Collections.Generic;
using UnityEngine;
using System.IO;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Inventory")]
public class InventoryManager : ScriptableObject
{
    [SerializeField]
    public List<string> itemNames = new List<string>(); // ������ �̸� ����Ʈ�� ����

    [SerializeField]
    public List<int> seeds = new List<int>();

    // �����͸� ������ ���� ���
    private string path;

    private void OnEnable()
    {
        // ���� ��� ����
        path = Path.Combine(Application.persistentDataPath, "inventory.json");
        JsonLoad();
    }

    // ������ �̸� �߰� �޼���
    public void AddItem(string itemName)
    {
        itemNames.Add(itemName);
    }

    // ������ �̸� ���� �޼���
    public void RemoveItem(string itemName)
    {
        itemNames.Remove(itemName);
    }

    // ���� �߰� �޼���
    public void AddSeed(int index, int value)
    {
        seeds[index] = value;
    }

    // ���� ���� �޼���
    public void RemoveSeed(int index, int value)
    {
        seeds[index] = value;
    }

    // JSON ���Ͽ��� ������ �ε�
    public void JsonLoad()
    {
        if (File.Exists(path))
        {
            string loadJson = File.ReadAllText(path);
            InventoryManagerData data = JsonUtility.FromJson<InventoryManagerData>(loadJson);

            Debug.Log("�ε� ���̽�" + loadJson);
            if (data != null)
            {
                itemNames = data.itemNames;
                seeds = data.seeds;
            }
        }
        else
        {
            Debug.LogWarning("Inventory file not found. Creating new inventory.");
        }
    }

    // JSON ���Ͽ� ������ ����
    public void JsonSave()
    {
        InventoryManagerData data = new InventoryManagerData();
        data.itemNames = itemNames;
        data.seeds = seeds;

        Debug.Log("���̺굥��Ÿ" + data);
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
    }


}

// JSON���� ������ ������ Ŭ����
[System.Serializable]
public class InventoryManagerData
{
    public List<string> itemNames = new List<string>();
    public List<int> seeds = new List<int>();
}
