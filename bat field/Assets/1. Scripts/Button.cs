using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Button : MonoBehaviour
{
    // ��ư Ȱ��ȭ�� ����
    [SerializeField] GameObject field;
    private bool fieldActive = false;// �ʵ� ui Ȱ��ȭ/��Ȱ��ȭ üũ

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
        // ���콺 Ŭ���� ��ġ
        Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // ����ĳ��Ʈ �߻�
        RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero);
        // �� ������Ʈ ����
        if (hit.collider != null)
        {
            // �浹�� ������Ʈ�� ���� ���
            // �� ������Ʈ ����
            GameObject hitObject = hit.collider.gameObject;
            if (hitObject.layer == LayerMask.NameToLayer("Field"))
            {
                // �ʵ� UI�� Ȱ��ȭ ���¸� ����
                fieldActive = !fieldActive;
                // �ʵ� UI Ȱ��ȭ ���¿� ���� ���� ������Ʈ�� Ȱ��ȭ ���� ����
                field.gameObject.SetActive(fieldActive);
            }
            // hitObject�� ����Ͽ� �ʿ��� �۾� ����
        }
    }
}
