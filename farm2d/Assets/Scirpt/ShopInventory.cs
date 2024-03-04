using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInventory : MonoBehaviour
{
    public static ShopInventory instance;
    private int slotCnt;

    public delegate void OnSlotCountChange(int val);
    public OnSlotCountChange onSlotCountChange;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        SlotCnt = 4;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int SlotCnt
    {
        get => slotCnt;
        set{
        slotCnt =value;
            onSlotCountChange.Invoke(slotCnt);
        }
    }
}
