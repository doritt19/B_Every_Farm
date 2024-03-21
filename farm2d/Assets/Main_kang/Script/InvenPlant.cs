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
    public string imagePath; // �̹��� ���

    // �̹��� �ε� �Լ�
    public Sprite LoadImageFromPath()
    {
        // Resources �������� �̹��� �ε�
        Sprite sprite = Resources.Load<Sprite>(imagePath);
        Debug.Log(sprite);
        return sprite;
    }
}
