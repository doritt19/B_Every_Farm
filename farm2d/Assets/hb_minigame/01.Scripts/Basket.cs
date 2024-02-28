using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fruit")
        {
            Debug.Log("충돌");
            // 충돌한 오브젝트가 "Fruit" 태그인 경우
            GameManager.instance.goldCount++; // 골드 카운트 증가
            GameManager.instance.goldText.text = GameManager.instance.goldCount.ToString(); // 골드 텍스트 업데이트
            Destroy(collision.gameObject);
        }
    }
}
