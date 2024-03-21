using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class GrowManager : MonoBehaviour
{
    public Sprite[] sprite;  // 스프라이트 배열
    private SpriteRenderer spriteRenderer;
    public float growTime = 0; // 작물을 심은 후 지난 시간 (한나수정: 작물 성장 시간 및 식물 구별 코드)
    public bool harvesting = false;  // 수확이 가능한 상태인지 확인하는 bool값
    private int test = 0;  // 테스트 변수
    private bool isGrowing = false;

    public InvenPlant invenPlant;
    private float nextTime = 0f; // 한나 수정, 작물 성장 누적 시간
    private Inventory inventory; // 인벤토리 스크립트
    private int currentIndex = 0; // 한나수정, 현재 스프라이트 번호
    private Animator childAnimator;// 자식 오브젝트의 애니메이션 컴포넌트에 접근하기 위한 변수
    private SpriteRenderer childSprite; // 자식 오브젝트 물방울의 스프라이트에 접근하기 위한 변수

    void Start()
    {
        nextTime = growTime;
        inventory = FindAnyObjectByType<Inventory>(); // 인벤토리 스크립트가있는 컴포넌트를 찾아서 가져오기


        spriteRenderer = GetComponent<SpriteRenderer>();

        // 물을 주고 성장 코드 실행
        // 자식 오브젝트의 애니메이션 및 스프라이트 컴포넌트 가져오기
        childAnimator = GetComponentInChildren<Animator>();
        childSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();

        //농작물을 심자마자 코루틴 시작
        // 한나 수정, 성장 함수화, 물주고 난후부터 성장시작
        //Grow();
    }
    /// <summary>
    /// 한나 수정
    /// 작물 성장시간이 다음 성장시간이 될때까지 시간 누적
    /// 클릭할때 해당 오브젝트의 마우스위치값을 받아 해당 오브젝트만 수확하게 변경
    /// </summary>
    void Update()
    {
        nextTime += Time.deltaTime;

        // 마우스 왼쪽 버튼이 눌렸는지 확인합니다.
        if (Input.GetMouseButtonDown(0))
        {
            // 마우스로 클릭한 위치를 스크린 좌표로부터 Ray를 통해 월드 좌표로 변환합니다.
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero);

            // Raycast를 사용하여 클릭된 객체를 판별합니다.
            if (hit.collider != null)
            {
                GameObject clickedObject = hit.collider.gameObject;

                // 클릭된 오브젝트가 자신인지 확인합니다.
                if (clickedObject == gameObject)
                {
                    // Debug.Log("자신을 클릭했습니다!");

                    // 물 애니메이션 실행

                    Waterplant();


                }
            }
        }



    }

    public void Grow()
    {
        Debug.Log("grow싱행");
        // 자신이 수확 가능한 상태이면
        if (harvesting && !inventory.inventoryFull)
        {
            // 수확물에 따른 경험치 획득
            PlayerPrefs.SetInt("ExpCount", PlayerPrefs.GetInt("ExpCount") + invenPlant.plantExp);
            PlayerPrefs.Save();

            // 인벤토리에 자신을 넣기
            inventory.AddItem(invenPlant);

            // 자신을 제거
            Destroy(gameObject);
            // 밭에서 자신의 위치를 제거하여 다시 심을 수 있게 초기화
            InventoryButton.tileCenterList.Remove(gameObject.transform.position);
        }
        // 물을 준 상태로 코르틴 실행
        if (spriteRenderer != null)
        {
            Debug.Log("성장해");
            StartCoroutine(ChangeSpriteWithDelay());

        }
        if (inventory.inventoryFull)
        {
            // 인벤토리가 꽉찼습니다
            Debug.Log("인벤토리가 꽉찼습니다");
        }
    }


    public void Waterplant()
    {

        // 애니메이션 컴포넌트가 존재하는지 확인
        if (childAnimator != null)
        {
            // 애니메이션 재생
            childAnimator.enabled = true;
            childSprite.enabled = true;
            //  Debug.Log("애니메이션 재생");

        }
        else
        {
            Debug.LogError("자식 오브젝트에 애니메이션 컴포넌트가 없습니다.");
        }

        StartCoroutine(WaterDelay()); // 물주는 시간 딜레이
        //자라는함수 실행
        Grow();
    }
    /// <summary>
    /// 애니메이션이 진행될때까지 기다리기
    /// </summary>
    /// <returns></returns>
    public IEnumerator WaterDelay()
    {
        yield return new WaitForSeconds(0.8f);
        Debug.Log("물준거 기다림");
        // 한나수정, 애니메이션 실행 및 물방울 스프라이트 비활성화 초기화
        childSprite.enabled = false;
        childAnimator.enabled = false;
    }

    private IEnumerator ChangeSpriteWithDelay()
    {
        if (isGrowing)
        {
            yield break;
        }
        if (currentIndex < sprite.Length) // 현재 인덱스가 스프라이트 배열보다 작다면
        {
            isGrowing = true;

            currentIndex++;

            yield return new WaitForSeconds(growTime); // 작물이 자라는 시간동안 기다리기

            spriteRenderer.sprite = sprite[currentIndex]; // 스프라이트 변경
            isGrowing = false;
            if (currentIndex == sprite.Length )
            {
                harvesting = true; // 마지막 스프라이트로 변경되었으면 수확이 가능한 상태로 변경
            }


            if (currentIndex >= sprite.Length)  yield break;  // 마지막 스프라이트로 변경되었으면 실행 종료
        }
    }
}
