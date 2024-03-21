using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Item")]
public class InvenPlant : ScriptableObject
{
    public string plantName;
    public int plantGold;
    public int invenNum;
    public int plantExp;
    public string imagePath; // 이미지 경로

    // 이미지 로드 함수
    public Sprite LoadImageFromPath()
    {
        // Resources 폴더에서 이미지 로드
        Sprite sprite = Resources.Load<Sprite>(imagePath);
        Debug.Log(sprite);
        return sprite;
    }
}
