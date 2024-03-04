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
//        //OnPointerDown 포인터가 오브젝트 위에서 눌렸을 때 호출
//    }

//    public void OnPointerUp(PointerEventData eventData)
//    {
//        controller.inputmove(0);
//        //OnPointerUp 포인터를 오브젝트 에서 뗄 때 호출
//    }
//}
