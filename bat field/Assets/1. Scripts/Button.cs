using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Button : MonoBehaviour
{
    // 버튼 활성화용 변수
    [SerializeField] GameObject field;
    private bool fieldActive = false;// 필드 ui 활성화/비활성화 체크

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            OpenField();
        }

    }
    public void OpenField()
    {
        // 마우스 클릭한 위치
        Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // 레이캐스트 발사
        RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero);
        // 밭 오브젝트 색출
        if (hit.collider != null)
        {
            // 충돌한 오브젝트가 있을 경우
            // 밭 오브젝트 색출
            GameObject hitObject = hit.collider.gameObject;
            if (hitObject.layer == LayerMask.NameToLayer("Field"))
            {
                // 필드 UI의 활성화 상태를 반전
                fieldActive = !fieldActive;
                // 필드 UI 활성화 상태에 따라 게임 오브젝트의 활성화 상태 설정
                field.gameObject.SetActive(fieldActive);
            }
            // hitObject를 사용하여 필요한 작업 수행
        }
    }
}
