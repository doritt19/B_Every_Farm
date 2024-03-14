using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellPlant : MonoBehaviour
{
    /// <summary>
    /// 작물을 판매하는 버튼과 스킬을 관리하는 스크립트
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
