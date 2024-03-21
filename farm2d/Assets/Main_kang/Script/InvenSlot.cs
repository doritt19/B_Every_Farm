using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvenSlot : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] int slotNum;

    private int index;
    private InvenPlant _plant;
    public InvenPlant plant
    {
        get { return _plant; }
        set
        {
            _plant = value;
            if (_plant != null)
            {
                image.color = new Color(1, 1, 1, 1);
                image.sprite = plant.LoadImageFromPath();
                slotNum = plant.invenNum;


            }
            else
            {
                image.color = new Color(1, 1, 1, 0);
                slotNum = -1;
            }
        }
    }
}