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
    public int gold = 50000;

    public bool isStoreActive= false;
    // Start is called before the first frame update
    void Start()
    {
    
        slots = slotHolder.GetComponentsInChildren<Slot>();
        SSU = this;
        loadedImage = new List<Sprite>(Resources.LoadAll<Sprite>("Images"));

        foreach (Sprite sprite in loadedImage)
        {
            // �ε�� Sprite�� ����ϰų� ���
            Debug.Log("Loaded Sprite: " + sprite.name);
        }

        shopPanel.SetActive(activeInventory);
        //closeShop.onClick.AddListener(DeActiveShop);
    }

    // Update is called once per frame
    
    void Update()
    {
        goldtext.GetComponent<TextMeshProUGUI>().text = gold.ToString() + " gold";
        // ��ġ �Է� Ȯ��
        //if (Input.touchCount > 0)
        //{
        //    Touch touch = Input.GetTouch(0); // ù ��° ��ġ�� ���

        //    // ��ġ�� ���۵� ��� (��ġ�� ù ������)
        //    if (touch.phase == TouchPhase.Began)
        //    {
        //        // ��ġ ��ǥ�� ��ũ�� ��ǥ�� ��ȯ
        //        Vector3 touchPosition = new Vector3(touch.position.x, touch.position.y, 0f);

        //        // Ray�� �����Ͽ� ��ġ ��ǥ�� ���.
        //        Ray ray = Camera.main.ScreenPointToRay(touchPosition);

        //        RaycastHit hit;

        //        // Ray�� �浹�� ���
        //        if (Physics.Raycast(ray, out hit))
        //        {
        //            // ���⿡�� hit ������ ����Ͽ� Ư�� ���� ����
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
       
    }
   
}
