using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvenSlot : MonoBehaviour
{
    [SerializeField] Image image;

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
                image.sprite = plant.plantImage;
              

            }
            else
            {
                image.color = new Color(1, 1, 1, 0);
            }
        }
    }
}