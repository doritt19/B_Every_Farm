//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.EventSystems;

//public class InputMR : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
//{
//    PlayerController controller;
//    // Start is called before the first frame update
//    void Start()
//    {
//        controller = FindObjectOfType<PlayerController>();
//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }

//    public void OnPointerDown(PointerEventData eventData)
//    {
//        controller.inputmove(1);
//        //OnPointerDown �����Ͱ� ������Ʈ ������ ������ �� ȣ��
//    }

//    public void OnPointerUp(PointerEventData eventData)
//    {
//        controller.inputmove(0);
//        //OnPointerUp �����͸� ������Ʈ ���� �� �� ȣ��
//    }
//}
