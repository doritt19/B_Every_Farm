using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SellPlant : MonoBehaviour
{
    private Inventory inventory; // 인벤토리 스크립트
    private InvenSlot invenSlot; // 슬롯스크립트
    public InvenPlant invenPlant;
    public Text sellText; // 패널에 띄울 팔 작물의 정보 텍스트를 담을 공간
    public GameObject sellPanel; // 셀 패널 담을 공간
      
    /// <summary>
    /// 작물을 판매하는 버튼과 스킬을 관리하는 스크립트
    /// </summary>


    private void Awake()
    {
        inventory = FindAnyObjectByType<Inventory>(); // 인벤토리 스크립트가있는 컴포넌트를 찾아서 가져오기
        invenSlot = GetComponentInChildren<InvenSlot>(); // 자식 컴포넌트에 있는 인벤 슬롯 컴포넌트 찾아서 가져오기
        
    }


    // Update is called once per frame
    public void Sellpanel()
    {
        sellPanel.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
        if (invenSlot.plant != null) // 인벤토리에 아이템이 있는 경우에만 처리합니다.
        {  // 기존에 등록된 모든 이벤트 제거
            

            // 새로운 판매패널 세팅
            invenPlant = invenSlot.plant;
            sellText.text = invenSlot.plant.plantName + " 을(를)\r\n판매하시겠습니까?\r\n+" + invenSlot.plant.plantGold.ToString() + "원";
            sellPanel.SetActive(true);
            sellPanel.GetComponentInChildren<Button>().onClick.AddListener(Sell);
        }
    }
    public void Sell() // 실제 판매 버튼을 눌렀을 때 실행될 함수
    {
        inventory.RemoveItem(invenPlant);
        invenPlant = null;
        sellPanel.SetActive(false);
    }
   

}
