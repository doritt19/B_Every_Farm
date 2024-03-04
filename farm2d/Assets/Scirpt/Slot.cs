using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Slot : MonoBehaviour
{
    public static Slot st;
    public bool isShopMode;
    public Button slotbutton;
    // Start is called before the first frame update
    void Start()
    {
        slotbutton.onClick.AddListener(OnButtonClick);
        st = this;
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void OnButtonClick()
    {
        if(ItemDataBase.instance.currentType!=ItemType.Main)
        {
            GameManager.GM.shoptest.SetActive(true);
        }
        else
        {
            GameManager.GM.shoptest.SetActive(false);
        }
    }
    
}
