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
        //if (collision.gameObject.tag == "Fruit")
        //{
        //MiniGameManager miniGameManager = MiniGameManager.instance;

        //    Debug.Log("충돌");
        //    // 충돌한 오브젝트가 "Fruit" 태그인 경우
        //    MiniGameManager.instance.goldCount++; // 골드 카운트 증가
        //    MiniGameManager.instance.goldText.text = MiniGameManager.instance.goldCount.ToString(); // 골드 텍스트 업데이트

        //    Destroy(collision.gameObject);
        //}
        if (collision.gameObject.tag == "Fruit")
        {
            Debug.Log("충돌");
            // 충돌한 오브젝트가 "Fruit" 태그인 경우
            MiniGameManager miniGameManager = MiniGameManager.instance;
            miniGameManager.AddGold(1); // 골드 카운트 증가

            int currentGoldCount = miniGameManager.goldCount;
           
            /* 경험치 증가에 관한 코드 ********수정 강한나 미니게임에선 경험치 획득 x
            if (currentGoldCount % 10 == 0)
            {
                // 10의 배수일 때 경험치 100 증가
                Debug.Log("경험치 +100");
                miniGameManager.UpdateExperience(100);
               
            }
            else
            {
                // 골드 카운트가 1마다 경험치 5씩 증가
                Debug.Log("경험치 +5");
                miniGameManager.UpdateExperience(5);
            }*/

            Destroy(collision.gameObject);
        }
    }
}
