using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SeedJsonData", menuName = "Inventory/SeedJson", order = 1)]
public class SeedJson : ScriptableObject
{
    [SerializeField]
    public List<Vector2> seedDrop = new List<Vector2>();
    public int[] sprites ;
    public List<string> seedNames ;

    private string path;
    // Start is called before the first frame update
    private void OnEnable()
    {
        seedNames.Clear();
        sprites = new int[64];
        for (int i = 0; i < 64; i++)
        {
            seedNames.Add(null); // string의 기본값 (null)으로 채웁니다.
        }
        path = Path.Combine(Application.persistentDataPath, "InventoryButton.json");
        seedDrop.Clear();


        //JsonSave();
        JsonLoad();
     
     
    }
    
    public void AddVector(float a, float b)
    {
        Vector2 newVector = new Vector2(a, b);
        seedDrop.Add(newVector);
    }
    public void RemoveVector(float a, float b)
    {
        Vector2 newVector = new Vector2(a, b);
        
        int index = seedDrop.IndexOf(newVector);
        seedDrop.Remove(newVector);
        sprites[index] = 0;
        seedNames[index] = null;
    }
    public void AddVage(string name,int a, float x, float y)
    {
        Vector2 newVector = new Vector2(x, y);
        int index = seedDrop.IndexOf(newVector); ;
        sprites[index] = a;
        seedNames[index] = name;
    }
   

  
  

    // Update is called once per frame
    public void JsonLoad()
    {
        if (File.Exists(path))
        {
            string loadJson = File.ReadAllText(path);
            SeedJsonData data = JsonUtility.FromJson<SeedJsonData>(loadJson);

            Debug.Log("로드 제이슨" + loadJson);

            if (data != null)
            {
                seedDrop = data.seed;
                sprites = data.Jsonsprites;
                seedNames = data.seedName;
            }
        }
        else
        {
            Debug.LogWarning("SeedDrop vector2 not found");
        }
    } 

    public void JsonSave()
    {
        SeedJsonData data = new SeedJsonData();
        data.seed = seedDrop;
        data.Jsonsprites = sprites;
        data.seedName = seedNames;
        
        string json = JsonUtility.ToJson(data,true);
        File.WriteAllText(path, json);
    }
    // 저장할 데이터 클래스
    [System.Serializable]
    public class SeedJsonData
    {
        public List<Vector2> seed = new List<Vector2>();
        public int[] Jsonsprites;
        public List<string> seedName;
    }
    
}
