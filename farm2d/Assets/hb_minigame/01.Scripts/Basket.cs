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

        //    Debug.Log("�浹");
        //    // �浹�� ������Ʈ�� "Fruit" �±��� ���
        //    MiniGameManager.instance.goldCount++; // ��� ī��Ʈ ����
        //    MiniGameManager.instance.goldText.text = MiniGameManager.instance.goldCount.ToString(); // ��� �ؽ�Ʈ ������Ʈ

        //    Destroy(collision.gameObject);
        //}
        if (collision.gameObject.tag == "Fruit")
        {
            Debug.Log("�浹");
            // �浹�� ������Ʈ�� "Fruit" �±��� ���
            MiniGameManager miniGameManager = MiniGameManager.instance;
            miniGameManager.AddGold(1); // ��� ī��Ʈ ����

            int currentGoldCount = miniGameManager.goldCount;
           
            /* ����ġ ������ ���� �ڵ� ********���� ���ѳ� �̴ϰ��ӿ��� ����ġ ȹ�� x
            if (currentGoldCount % 10 == 0)
            {
                // 10�� ����� �� ����ġ 100 ����
                Debug.Log("����ġ +100");
                miniGameManager.UpdateExperience(100);
               
            }
            else
            {
                // ��� ī��Ʈ�� 1���� ����ġ 5�� ����
                Debug.Log("����ġ +5");
                miniGameManager.UpdateExperience(5);
            }*/

            Destroy(collision.gameObject);
        }
    }
}
