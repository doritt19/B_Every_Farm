using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowManager : MonoBehaviour
{
    public Sprite[] sprite;  // 스프라이트 배열
    private SpriteRenderer spriteRenderer;
    public float glowTime = 0; // 작물을 심은 후 지난 시간
    public bool harvesting = false;  // 수확이 가능한 상태인지 확인하는 bool값
    private int test = 0;  // 테스트 변수

    void Start()
    {
        //농작물을 심자마자 코루틴 시작
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            StartCoroutine(ChangeSpriteWithDelay());
        }
    }

    void Update()
    {
        glowTime += Time.deltaTime;

        if (harvesting && Input.GetMouseButtonDown(0))
        {
            test++; //클릭하여 작물을 수확시 경험치와 돈을 획득하는 코드
            Destroy(gameObject);
        }
    }

    private IEnumerator ChangeSpriteWithDelay()
    {
        int currentIndex = 0;
        while (currentIndex < sprite.Length) // 현재 인덱스가 스프라이트 배열보다 작다면
        {
            yield return new WaitForSeconds(2f); // 작물이 자라는 시간

            spriteRenderer.sprite = sprite[currentIndex]; // 스프라이트 변경

            if (currentIndex == sprite.Length - 1)
            {
                harvesting = true; // 마지막 스프라이트로 변경되었으면 수확이 가능한 상태로 변경
            }

            currentIndex++;

            if (currentIndex >= sprite.Length)
                yield break;  // 마지막 스프라이트로 변경되었으면 실행 종료
        }
    }
}
