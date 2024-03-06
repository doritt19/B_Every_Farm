using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.EventSystems.StandaloneInputModule;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // 플레이어 이동 속도
    

    private Rigidbody2D rb; // 플레이어 리지드바디
    private Animator animator; // 플레이어 애니메이터
    private SpriteRenderer spriteRenderer; // 플레이어 스프라이트렌더러
    public Transform leftBound; // 왼쪽 제한 위치
    public Transform rightBound; // 오른쪽 제한 위치
   

    private bool moveLeft = false; // 왼쪽 이동 여부
    private bool moveRight = false; // 오른쪽 이동 여부
    private float moveInput;

    void Start()
    {
        // 컴포넌트 초기화
        rb = GetComponent<Rigidbody2D>(); 
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        moveLeft = false;
        moveRight = false;  
    }

    void Update()
    {

        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, leftBound.position.x, rightBound.position.x);
        transform.position = clampedPosition;
        // 키보드 입력을 받아 플레이어 이동
        float horizontalInput = Input.GetAxis("Horizontal");

        // 이동 방향에 따라 플레이어 이동
        transform.Translate(new Vector3(horizontalInput * moveSpeed * Time.deltaTime, 0f, 0f));
        // 입력에 따라 플레이어가 좌우 방향을 바라보도록 설정
        if (horizontalInput > 0) // 왼른쪽으로 이동하는 경우
        {
            transform.localScale = new Vector3(-1f, 1f, 1f); // 플레이어 스케일을 왼쪽 방향으로 설정
        }
        else if (horizontalInput < 0) // 오른쪽으로 이동하는 경우
        {
            transform.localScale = new Vector3(1f, 1f, 1f); // 플레이어를 좌우 반전하여 오른 방향으로 설정
        }
    }
    public void inputmove(float moveInputs)
    {
      

            moveInput = moveInputs*moveSpeed;
       

        // 플레이어 이동
        Vector2 moveVelocity = new Vector2(moveInput, 0);
        
        rb.velocity = moveVelocity;

     

        // 플레이어가 좌우 방향을 바라보도록 설정
        if (moveInput < 0)
        {
            spriteRenderer.flipX = false;
            animator.SetTrigger("Letf"); // 애니메이션 트리거 설정
        }
        else if (moveInput > 0)
        {
            spriteRenderer.flipX = true;
            animator.SetTrigger("Right"); // 애니메이션 트리거 설정
        }
    }
    // 왼쪽 버튼 눌림 여부 갱신
    public void OnLeftButtonDown()
    {
        moveLeft = true;
        if (moveLeft)
        {
           
        }
       
     
        Debug.Log("왼쪽버튼눌러짐");
    }

    // 왼쪽 버튼 떼어짐 여부 갱신
    public void OnLeftButtonUp()
    {
        moveLeft = false;
        Debug.Log("왼쪽버튼떼어짐");
    }

    // 오른쪽 버튼 눌림 여부 갱신
    public void OnRightButtonDown()
    {
        inputmove(1);
        moveRight = true;
        Debug.Log("오른쪽버튼눌러짐");
    }

    // 오른쪽 버튼 떼어짐 여부 갱신
    public void OnRightButtonUp()
    {
      
        Debug.Log("오른쪽버튼떼어짐");
    }

  
    
}