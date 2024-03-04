using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[System.Serializable]
public class TextArray
{
    public string[] talk; //��� �迭
}

public class TextManager : MonoBehaviour
{
    public Text talkText; //���
    public GameObject npcImg; //NPC�̹���
    public GameObject arrowImg; //��ȭ ���Ḧ �˸��� ȭ��ǥ �̹���
    public GameObject chatWindow; //��ȭâ
    public GameObject windowShadow; //��ȭ���� ȭ�� ����ȿ��

    public int currentTextIndex = 0;
    public float typingSpeed = 0.05f; //Ÿ���θ�� �ӵ�
    private bool isTyping = false; //���� Ÿ���� ����� Ȱ��ȭ������ Ȯ��
    public int chapterNumber = 0; //���� é��
    public TextArray[] Chapter; //é�� �迭

    private bool[] isChapter; //é�Ͱ� ������ �Ǿ����� üũ
    private EventSystem eventSystem; //��ȭ���� �̺�Ʈ�ý��� �۵��� ����


    public void Start()
    {
        //������ �������ڸ��� UI�� ���̴� ���� ����
        npcImg.SetActive(false);
        arrowImg.SetActive(false);
        chatWindow.SetActive(false);
        talkText.gameObject.SetActive(false);
        windowShadow.SetActive(false);

        isChapter = new bool[Chapter.Length]; //isChapter�迭 �ʱ�ȭ
        for (int i = 0; i < isChapter.Length; i++)
        {
            isChapter[i] = false;
        }

        eventSystem = EventSystem.current;
    }

    private void Update()
    {
        textLoad();
    }

    public void textLoad()
    {
        if (Input.GetMouseButtonDown(0)) //talk�� �迭�� 0���� �ƴ϶�� Ŭ������ ��ȭ �Ѿ
        {
            if (!isTyping) // Ÿ���θ������ �ƴ� ��
            {
                if (currentTextIndex < Chapter[chapterNumber].talk.Length)
                {
                    // ��ȭ ���� �� �ؽ�Ʈ�� Ÿ���θ�� ȿ��
                    StartCoroutine(TypeText(Chapter[chapterNumber].talk[currentTextIndex]));
                    currentTextIndex++;

                    // ��ȭ ���� �� ��ȭâ UI Ȱ��ȭ
                    npcImg.SetActive(true);
                    chatWindow.SetActive(true);
                    talkText.gameObject.SetActive(true);
                    arrowImg.SetActive(false);
                    windowShadow.SetActive(true);

                    // �̺�Ʈ �ý��� ���
                    LockEventSystem(true);
                }
                else
                {
                    // ��ȭ ���� �� ��ȭâ UI ��Ȱ��ȭ
                    npcImg.SetActive(false);
                    arrowImg.SetActive(false);
                    chatWindow.SetActive(false);
                    talkText.gameObject.SetActive(false);
                    windowShadow.SetActive(false);

                    // �̺�Ʈ �ý��� ��� ����
                    LockEventSystem(false);
                }
            }
            else // Ÿ���θ�� ���� Ŭ�� ��
            {
                // Ŭ�� �Է����� Ÿ���� ��� ����
                StopAllCoroutines();
                talkText.text = Chapter[chapterNumber].talk[currentTextIndex - 1];
                arrowImg.SetActive(true);
                isTyping = false;
            }
        }
        else // talk�� �迭�� 0��°�� ��쿡�� �ٷ� ����
        {
            if (!isTyping && currentTextIndex == 0)
            {
                if (Chapter[chapterNumber].talk.Length > 0)
                {
                    StartCoroutine(TypeText(Chapter[chapterNumber].talk[currentTextIndex]));
                    currentTextIndex++;

                    // ��ȭ ���� �� ��ȭâ UI Ȱ��ȭ
                    npcImg.SetActive(true);
                    chatWindow.SetActive(true);
                    talkText.gameObject.SetActive(true);
                    arrowImg.SetActive(false);
                    windowShadow.SetActive(true);

                    // �̺�Ʈ �ý��� ���
                    LockEventSystem(true);
                }
            }
        }
    }

    public void MenueChapter()
    {
        if (!isChapter[1]) //é�Ͱ� ����Ǿ����� Ȯ��
        {
            chapterNumber = 1;
            currentTextIndex = 0;
            isChapter[1] = true; //é�Ͱ� ����Ǿ����� Ȯ��
        }
    }

    public void InventoryChapter()
    {
        if (!isChapter[2]) //é�Ͱ� ����Ǿ����� Ȯ��
        {
            chapterNumber = 2;
            currentTextIndex = 0;
            isChapter[2] = true; //é�Ͱ� ����Ǿ����� Ȯ��
        }
    }

    IEnumerator TypeText(string textToType) //Ÿ���θ��
    {
        if (textToType == null)
        {
            yield return null;
        }
        isTyping = true;
        talkText.text = "";
        foreach (char letter in textToType)
        {
            talkText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        arrowImg.SetActive(true);
        isTyping = false;
    }

    private void LockEventSystem(bool lockState)
    {
        if (eventSystem != null)
        {
            eventSystem.enabled = !lockState;
        }
    }
}
