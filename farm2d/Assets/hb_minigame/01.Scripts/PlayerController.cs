using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.EventSystems.StandaloneInputModule;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // �÷��̾� �̵� �ӵ�
    

    private Rigidbody2D rb; // �÷��̾� ������ٵ�
    private Animator animator; // �÷��̾� �ִϸ�����
    private SpriteRenderer spriteRenderer; // �÷��̾� ��������Ʈ������
    public Transform leftBound; // ���� ���� ��ġ
    public Transform rightBound; // ������ ���� ��ġ
   

    private bool moveLeft = false; // ���� �̵� ����
    private bool moveRight = false; // ������ �̵� ����
    private float moveInput;

    void Start()
    {
        // ������Ʈ �ʱ�ȭ
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
        // Ű���� �Է��� �޾� �÷��̾� �̵�
        float horizontalInput = Input.GetAxis("Horizontal");

        // �̵� ���⿡ ���� �÷��̾� �̵�
        transform.Translate(new Vector3(horizontalInput * moveSpeed * Time.deltaTime, 0f, 0f));
        // �Է¿� ���� �÷��̾ �¿� ������ �ٶ󺸵��� ����
        if (horizontalInput > 0) // �޸������� �̵��ϴ� ���
        {
            transform.localScale = new Vector3(-1f, 1f, 1f); // �÷��̾� �������� ���� �������� ����
        }
        else if (horizontalInput < 0) // ���������� �̵��ϴ� ���
        {
            transform.localScale = new Vector3(1f, 1f, 1f); // �÷��̾ �¿� �����Ͽ� ���� �������� ����
        }
    }
    public void inputmove(float moveInputs)
    {
      

            moveInput = moveInputs*moveSpeed;
       

        // �÷��̾� �̵�
        Vector2 moveVelocity = new Vector2(moveInput, 0);
        
        rb.velocity = moveVelocity;

     

        // �÷��̾ �¿� ������ �ٶ󺸵��� ����
        if (moveInput < 0)
        {
            spriteRenderer.flipX = false;
            animator.SetTrigger("Letf"); // �ִϸ��̼� Ʈ���� ����
        }
        else if (moveInput > 0)
        {
            spriteRenderer.flipX = true;
            animator.SetTrigger("Right"); // �ִϸ��̼� Ʈ���� ����
        }
    }
    // ���� ��ư ���� ���� ����
    public void OnLeftButtonDown()
    {
        moveLeft = true;
        if (moveLeft)
        {
           
        }
       
     
        Debug.Log("���ʹ�ư������");
    }

    // ���� ��ư ������ ���� ����
    public void OnLeftButtonUp()
    {
        moveLeft = false;
        Debug.Log("���ʹ�ư������");
    }

    // ������ ��ư ���� ���� ����
    public void OnRightButtonDown()
    {
        inputmove(1);
        moveRight = true;
        Debug.Log("�����ʹ�ư������");
    }

    // ������ ��ư ������ ���� ����
    public void OnRightButtonUp()
    {
      
        Debug.Log("�����ʹ�ư������");
    }

  
    
}