using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SellPlant : MonoBehaviour
{
    private Inventory inventory; // �κ��丮 ��ũ��Ʈ
    private InvenSlot invenSlot; // ���Խ�ũ��Ʈ
    public InvenPlant invenPlant;
    public Text sellText; // �гο� ��� �� �۹��� ���� �ؽ�Ʈ�� ���� ����
    public GameObject sellPanel; // �� �г� ���� ����
      
    /// <summary>
    /// �۹��� �Ǹ��ϴ� ��ư�� ��ų�� �����ϴ� ��ũ��Ʈ
    /// </summary>


    private void Awake()
    {
        inventory = FindAnyObjectByType<Inventory>(); // �κ��丮 ��ũ��Ʈ���ִ� ������Ʈ�� ã�Ƽ� ��������
        invenSlot = GetComponentInChildren<InvenSlot>(); // �ڽ� ������Ʈ�� �ִ� �κ� ���� ������Ʈ ã�Ƽ� ��������
        
    }


    // Update is called once per frame
    public void Sellpanel()
    {
        sellPanel.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
        if (invenSlot.plant != null) // �κ��丮�� �������� �ִ� ��쿡�� ó���մϴ�.
        {  // ������ ��ϵ� ��� �̺�Ʈ ����
            

            // ���ο� �Ǹ��г� ����
            invenPlant = invenSlot.plant;
            sellText.text = invenSlot.plant.plantName + " ��(��)\r\n�Ǹ��Ͻðڽ��ϱ�?\r\n+" + invenSlot.plant.plantGold.ToString() + "��";
            sellPanel.SetActive(true);
            sellPanel.GetComponentInChildren<Button>().onClick.AddListener(Sell);
        }
    }
    public void Sell() // ���� �Ǹ� ��ư�� ������ �� ����� �Լ�
    {
        inventory.RemoveItem(invenPlant);
        invenPlant = null;
        sellPanel.SetActive(false);
    }
   

}
