using System.Collections.Generic;
using UnityEngine;
using System.IO;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Inventory")]
public class InventoryManager : ScriptableObject
{
    [SerializeField]
    public List<string> itemNames = new List<string>(); // 아이템 이름 리스트로 변경

    [SerializeField]
    public List<int> seeds = new List<int>();

    // 데이터를 저장할 파일 경로
    private string path;

    private void OnEnable()
    {
        // 파일 경로 설정
        path = Path.Combine(Application.persistentDataPath, "inventory.json");
        JsonLoad();
    }

    // 아이템 이름 추가 메서드
    public void AddItem(string itemName)
    {
        itemNames.Add(itemName);
    }

    // 아이템 이름 제거 메서드
    public void RemoveItem(string itemName)
    {
        itemNames.Remove(itemName);
    }

    // 씨앗 추가 메서드
    public void AddSeed(int index, int value)
    {
        seeds[index] = value;
    }

    // 씨앗 제거 메서드
    public void RemoveSeed(int index, int value)
    {
        seeds[index] = value;
    }

    // JSON 파일에서 데이터 로드
    public void JsonLoad()
    {
        if (File.Exists(path))
        {
            string loadJson = File.ReadAllText(path);
            InventoryManagerData data = JsonUtility.FromJson<InventoryManagerData>(loadJson);

            Debug.Log("로드 제이슨" + loadJson);
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

    // JSON 파일에 데이터 저장
    public void JsonSave()
    {
        InventoryManagerData data = new InventoryManagerData();
        data.itemNames = itemNames;
        data.seeds = seeds;

        Debug.Log("세이브데이타" + data);
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
    }


}

// JSON으로 저장할 데이터 클래스
[System.Serializable]
public class InventoryManagerData
{
    public List<string> itemNames = new List<string>();
    public List<int> seeds = new List<int>();
}
