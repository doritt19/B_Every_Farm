using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopScriptUI : MonoBehaviour
{
    public static ShopScriptUI SSU;
    public GameObject shopPanel;
    bool activeInventory = false;
    public Slot[] slots;
    public Transform slotHolder;
    public Button closeShop;
    public List<Sprite> loadedImage;
    public GameObject goldtext;
    public static int gold; // 초기값 골드 변경필요

    public bool isStoreActive= false;
    // Start is called before the first frame update
    void Start()
    {
        gold = PlayerPrefs.GetInt(GameManager.goldCountKey);


        slots = slotHolder.GetComponentsInChildren<Slot>();
        SSU = this;
        loadedImage = new List<Sprite>(Resources.LoadAll<Sprite>("Images"));

        foreach (Sprite sprite in loadedImage)
        {
            // 로드된 Sprite를 사용하거나 출력
            Debug.Log("Loaded Sprite: " + sprite.name);
        }

        shopPanel.SetActive(activeInventory);
        //closeShop.onClick.AddListener(DeActiveShop);
    }

    // Update is called once per frame
    
    void Update()
    {
        goldtext.GetComponent<TextMeshProUGUI>().text = gold.ToString() + " gold";
        // 터치 입력 확인
        //if (Input.touchCount > 0)
        //{
        //    Touch touch = Input.GetTouch(0); // 첫 번째 터치만 고려

        //    // 터치가 시작된 경우 (터치의 첫 프레임)
        //    if (touch.phase == TouchPhase.Began)
        //    {
        //        // 터치 좌표를 스크린 좌표로 변환
        //        Vector3 touchPosition = new Vector3(touch.position.x, touch.position.y, 0f);

        //        // Ray를 생성하여 터치 좌표로 쏜다.
        //        Ray ray = Camera.main.ScreenPointToRay(touchPosition);

        //        RaycastHit hit;

        //        // Ray와 충돌한 경우
        //        if (Physics.Raycast(ray, out hit))
        //        {
        //            // 여기에서 hit 정보를 사용하여 특정 동작 수행
        //            Debug.Log("Ray hit: " + hit.collider.gameObject.name);
        //        }
        //    }
        //}
        //if (Input.GetMouseButtonUp(0) && !isStoreActive)
        //{
        //    isStoreActive = true;
        //    RayShop();
        
        //}
     
    }

    public void RayShop()
    {
        shopPanel.SetActive(true);
        //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //mousePos.z = -10;
        //if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(0))
        //{
        //    RaycastHit2D hit2D = Physics2D.Raycast(mousePos, transform.forward, 30);
        //    Debug.DrawRay(mousePos, transform.forward * 120, Color.red);
        //    if (hit2D.collider != null)
        //    {
        //        if (hit2D.collider.CompareTag("Store"))
        //        {

        //            shopPanel.SetActive(true);
        //        }
        //    }
        //}
    }
    public void DeActiveShop()
    {
        shopPanel.SetActive(false);
        ItemDataBase.instance.currentType=ItemType.Main;
        SlotManager.GM.shoptest.SetActive(false);

        SaveShopData();
    }
    public void SaveShopData()
    {
        // 한나 수정 상점 닫을때 남은 골드를 저장
        PlayerPrefs.SetInt(GameManager.goldCountKey, gold);
        PlayerPrefs.Save();

        // 구매한 아이템을 인벤토리에 반영하는 코드 필요
    }
}
