using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Item")]
public class InvenPlant : ScriptableObject
{
    public string plantName;
    public Sprite plantImage;
    public int plantGold;
    public int plantNum;

}

