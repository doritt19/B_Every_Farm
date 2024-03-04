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
        // shoptest 오브젝트의 Transform 컴포넌트를 가져오기
        Transform shoptestTransform = shoptest.transform;

        // 부모 오브젝트의 자식 수를 가져오기
        int childCount = shoptestTransform.childCount;

        // slotInfo 배열 초기화
        slotInfo = new Transform[childCount];

        // 모든 자식 오브젝트에 대해 반복
        for (int i = 0; i < childCount; i++)
        {
            // 특정 인덱스의 자식 Transform 가져오기
            slotInfo[i] = shoptestTransform.GetChild(i);
        }
    }
    
}
