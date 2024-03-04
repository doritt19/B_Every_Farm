using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager GM;
    public Transform[] slotInfo;
    public GameObject[] parentObjects;
    public GameObject shoptest;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
        GM = this;
        Information();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Information()
    {
        // shoptest ������Ʈ�� Transform ������Ʈ�� ��������
        Transform shoptestTransform = shoptest.transform;

        // �θ� ������Ʈ�� �ڽ� ���� ��������
        int childCount = shoptestTransform.childCount;

        // slotInfo �迭 �ʱ�ȭ
        slotInfo = new Transform[childCount];

        // ��� �ڽ� ������Ʈ�� ���� �ݺ�
        for (int i = 0; i < childCount; i++)
        {
            // Ư�� �ε����� �ڽ� Transform ��������
            slotInfo[i] = shoptestTransform.GetChild(i);
        }
    }
    
}
