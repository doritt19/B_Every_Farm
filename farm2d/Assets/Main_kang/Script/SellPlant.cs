using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellPlant : MonoBehaviour
{
    /// <summary>
    /// �۹��� �Ǹ��ϴ� ��ư�� ��ų�� �����ϴ� ��ũ��Ʈ
    /// </summary>

    public Sprite slotImage;
    // Start is called before the first frame update
    void Start()
    {
        slotImage = GetComponentInChildren<Sprite>();
    }

    // Update is called once per frame
    public void Sell()
    {
        
    }
}
