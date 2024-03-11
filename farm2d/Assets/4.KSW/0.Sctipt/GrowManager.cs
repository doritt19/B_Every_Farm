using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowManager : MonoBehaviour
{
    public Sprite[] sprite;  // ��������Ʈ �迭
    private SpriteRenderer spriteRenderer;
    public float glowTime = 0; // �۹��� ���� �� ���� �ð�
    public bool harvesting = false;  // ��Ȯ�� ������ �������� Ȯ���ϴ� bool��
    private int test = 0;  // �׽�Ʈ ����

    void Start()
    {
        //���۹��� ���ڸ��� �ڷ�ƾ ����
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
            test++; //Ŭ���Ͽ� �۹��� ��Ȯ�� ����ġ�� ���� ȹ���ϴ� �ڵ�
            Destroy(gameObject);
        }
    }

    private IEnumerator ChangeSpriteWithDelay()
    {
        int currentIndex = 0;
        while (currentIndex < sprite.Length) // ���� �ε����� ��������Ʈ �迭���� �۴ٸ�
        {
            yield return new WaitForSeconds(2f); // �۹��� �ڶ�� �ð�

            spriteRenderer.sprite = sprite[currentIndex]; // ��������Ʈ ����

            if (currentIndex == sprite.Length - 1)
            {
                harvesting = true; // ������ ��������Ʈ�� ����Ǿ����� ��Ȯ�� ������ ���·� ����
            }

            currentIndex++;

            if (currentIndex >= sprite.Length)
                yield break;  // ������ ��������Ʈ�� ����Ǿ����� ���� ����
        }
    }
}
