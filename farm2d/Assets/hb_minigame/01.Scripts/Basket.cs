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
            Debug.Log("�浹");
            // �浹�� ������Ʈ�� "Fruit" �±��� ���
            GameManager.instance.goldCount++; // ��� ī��Ʈ ����
            GameManager.instance.goldText.text = GameManager.instance.goldCount.ToString(); // ��� �ؽ�Ʈ ������Ʈ
            Destroy(collision.gameObject);
        }
    }
}
