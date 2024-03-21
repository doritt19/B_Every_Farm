using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class GrowManager : MonoBehaviour
{
    public Sprite[] sprite;  // ��������Ʈ �迭
    private SpriteRenderer spriteRenderer;
    public float growTime = 0; // �۹��� ���� �� ���� �ð� (�ѳ�����: �۹� ���� �ð� �� �Ĺ� ���� �ڵ�)
    public bool harvesting = false;  // ��Ȯ�� ������ �������� Ȯ���ϴ� bool��
    private int test = 0;  // �׽�Ʈ ����
    private bool isGrowing = false;

    public InvenPlant invenPlant;
    private float nextTime = 0f; // �ѳ� ����, �۹� ���� ���� �ð�
    private Inventory inventory; // �κ��丮 ��ũ��Ʈ
    private int currentIndex = 0; // �ѳ�����, ���� ��������Ʈ ��ȣ
    private Animator childAnimator;// �ڽ� ������Ʈ�� �ִϸ��̼� ������Ʈ�� �����ϱ� ���� ����
    private SpriteRenderer childSprite; // �ڽ� ������Ʈ ������� ��������Ʈ�� �����ϱ� ���� ����

    void Start()
    {
        nextTime = growTime;
        inventory = FindAnyObjectByType<Inventory>(); // �κ��丮 ��ũ��Ʈ���ִ� ������Ʈ�� ã�Ƽ� ��������


        spriteRenderer = GetComponent<SpriteRenderer>();

        // ���� �ְ� ���� �ڵ� ����
        // �ڽ� ������Ʈ�� �ִϸ��̼� �� ��������Ʈ ������Ʈ ��������
        childAnimator = GetComponentInChildren<Animator>();
        childSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();

        //���۹��� ���ڸ��� �ڷ�ƾ ����
        // �ѳ� ����, ���� �Լ�ȭ, ���ְ� ���ĺ��� �������
        //Grow();
    }
    /// <summary>
    /// �ѳ� ����
    /// �۹� ����ð��� ���� ����ð��� �ɶ����� �ð� ����
    /// Ŭ���Ҷ� �ش� ������Ʈ�� ���콺��ġ���� �޾� �ش� ������Ʈ�� ��Ȯ�ϰ� ����
    /// </summary>
    void Update()
    {
        nextTime += Time.deltaTime;

        // ���콺 ���� ��ư�� ���ȴ��� Ȯ���մϴ�.
        if (Input.GetMouseButtonDown(0))
        {
            // ���콺�� Ŭ���� ��ġ�� ��ũ�� ��ǥ�κ��� Ray�� ���� ���� ��ǥ�� ��ȯ�մϴ�.
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero);

            // Raycast�� ����Ͽ� Ŭ���� ��ü�� �Ǻ��մϴ�.
            if (hit.collider != null)
            {
                GameObject clickedObject = hit.collider.gameObject;

                // Ŭ���� ������Ʈ�� �ڽ����� Ȯ���մϴ�.
                if (clickedObject == gameObject)
                {
                    // Debug.Log("�ڽ��� Ŭ���߽��ϴ�!");

                    // �� �ִϸ��̼� ����

                    Waterplant();


                }
            }
        }



    }

    public void Grow()
    {
        Debug.Log("grow����");
        // �ڽ��� ��Ȯ ������ �����̸�
        if (harvesting && !inventory.inventoryFull)
        {
            // ��Ȯ���� ���� ����ġ ȹ��
            PlayerPrefs.SetInt("ExpCount", PlayerPrefs.GetInt("ExpCount") + invenPlant.plantExp);
            PlayerPrefs.Save();

            // �κ��丮�� �ڽ��� �ֱ�
            inventory.AddItem(invenPlant);

            // �ڽ��� ����
            Destroy(gameObject);
            // �翡�� �ڽ��� ��ġ�� �����Ͽ� �ٽ� ���� �� �ְ� �ʱ�ȭ
            InventoryButton.tileCenterList.Remove(gameObject.transform.position);
        }
        // ���� �� ���·� �ڸ�ƾ ����
        if (spriteRenderer != null)
        {
            Debug.Log("������");
            StartCoroutine(ChangeSpriteWithDelay());

        }
        if (inventory.inventoryFull)
        {
            // �κ��丮�� ��á���ϴ�
            Debug.Log("�κ��丮�� ��á���ϴ�");
        }
    }


    public void Waterplant()
    {

        // �ִϸ��̼� ������Ʈ�� �����ϴ��� Ȯ��
        if (childAnimator != null)
        {
            // �ִϸ��̼� ���
            childAnimator.enabled = true;
            childSprite.enabled = true;
            //  Debug.Log("�ִϸ��̼� ���");

        }
        else
        {
            Debug.LogError("�ڽ� ������Ʈ�� �ִϸ��̼� ������Ʈ�� �����ϴ�.");
        }

        StartCoroutine(WaterDelay()); // ���ִ� �ð� ������
        //�ڶ���Լ� ����
        Grow();
    }
    /// <summary>
    /// �ִϸ��̼��� ����ɶ����� ��ٸ���
    /// </summary>
    /// <returns></returns>
    public IEnumerator WaterDelay()
    {
        yield return new WaitForSeconds(0.8f);
        Debug.Log("���ذ� ��ٸ�");
        // �ѳ�����, �ִϸ��̼� ���� �� ����� ��������Ʈ ��Ȱ��ȭ �ʱ�ȭ
        childSprite.enabled = false;
        childAnimator.enabled = false;
    }

    private IEnumerator ChangeSpriteWithDelay()
    {
        if (isGrowing)
        {
            yield break;
        }
        if (currentIndex < sprite.Length) // ���� �ε����� ��������Ʈ �迭���� �۴ٸ�
        {
            isGrowing = true;

            currentIndex++;

            yield return new WaitForSeconds(growTime); // �۹��� �ڶ�� �ð����� ��ٸ���

            spriteRenderer.sprite = sprite[currentIndex]; // ��������Ʈ ����
            isGrowing = false;
            if (currentIndex == sprite.Length )
            {
                harvesting = true; // ������ ��������Ʈ�� ����Ǿ����� ��Ȯ�� ������ ���·� ����
            }


            if (currentIndex >= sprite.Length)  yield break;  // ������ ��������Ʈ�� ����Ǿ����� ���� ����
        }
    }
}
