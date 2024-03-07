using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // UI �� �������ؼ� ����Ƽ ������ �ý��� �߰�

public class MainUI : MonoBehaviour
{
    /*
     *  ���� UI ���� 4���� �� �μ� UI 3����
     *  ���� ��� : ������ ���� ǥ��( ����� �̸�, ����ġ, ��)
     *  ���� �ϴ� : ����� ��ų ��ư
     *      - ������ �ڽ� UI�� �θ� UI�� ��ư ������ �� Ȱ��ȭ
     *  ���� ��� : ���� ��¥ �ð� ���� 
     *  ���� �ϴ� : �޴��� 
     *      - �޴� ��ư�� ������ �޴��� �ڽ� UI�� Ȱ��ȭ
     *          - �޴����� �� ��ư���� ���� UI Ȱ��ȭ
     */

    public GameObject profilePanel; // ������ ���� �г�
    public Text moneytext; // ������ �Ӵ� ���� �ؽ�Ʈ (hb)
    public Text exptext; // ������ ����ġ ���� �ؽ�Ʈ

    public GameObject inventoryPanel; // ��ų�� ���� ��ư �г�

    public GameObject menuPanel; // �޴� ��
    public Button[] menuButtons; // �޴� ���� ��ư�� ���� ����Ʈ
    public GameObject settingPanel; // �ɼ� �г�
    public GameObject minigamePanel; // �̴ϰ��� ���� �ȳ� �г�


    public Text InfoDate; // ��¥ ���� �ؽ�Ʈ
    public Text InfoTime; // �ð� ���� �ؽ�Ʈ
    public Image InfoWether; // ���� ���� �̹���

    // Start is called before the first frame update
    private void Awake()
    {   
    }
    void Start()
    {
        menuPanel.SetActive(false);
        inventoryPanel.SetActive(false);
        settingPanel.SetActive(false);
        GetCurrentExp();
        GetCurrentDate();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetCurrentExp();
        GetCurrentDate();
    }

    // �޴��� Ȱ��ȭ ��ư �ż���
    public void MenuOpen()
    {
        if (menuPanel != null) // �޴� �г� ������Ʈ�� �Ҵ�Ǿ�������
        {
            if (menuPanel.activeSelf) // �޴��� ������Ʈ�� ���� ������
            {
                menuPanel.SetActive(false); // �޴��ٸ� ����

            }
            else // �޴��ٰ� ����������
            {
                menuPanel.SetActive(true); // �޴��ٸ� �ѱ�
            }
        }
       
    }

   
    // �޴����� ��ư Ŭ���� ȣ��Ǵ� �ż���
    public void MenuButton(int buttonIndex) 
    {
        // ��ư�� �ε����� ���� ����ġ ������ �޼��� ȣ��
        switch (buttonIndex)
        {
            case 0:

                if (minigamePanel.activeSelf) // �����г� ������Ʈ�� ���� ������
                {
                    minigamePanel.SetActive(false); // �����г��� ����

                }
                else // �޴��ٰ� ����������
                {
                    minigamePanel.SetActive(true); // �����г��� �ѱ�
                    minigamePanel.GetComponentInChildren<Text>().text = "�̴ϰ��� ���� Ƚ��\r\n" + GameManager.minigameCount.ToString();
                }
                /*
                 *  �̴ϰ��� �� �����ϴ� �ڵ� ����ֱ�
                 */
                break;
            case 1:

                if (inventoryPanel.activeSelf) // �����г� ������Ʈ�� ���� ������
                {
                    inventoryPanel.SetActive(false); // �����г��� ����

                }
                else // �޴��ٰ� ����������
                {
                    inventoryPanel.SetActive(true); // �����г��� �ѱ�
                }

                break;
            case 2:
                /*
                 * ������Ʈ ��ġ�� ���������
                 * �����ϴ� ���������
                 * �޼��� �Լ� ����ֱ�
                 */

                break;
            case 3:
                /* ���� â �г� �ѱ� ���� 
                 * 
                 *  �̰��� ����Ʈ â �г� UI�� Ű�� ����
                 *  �ڵ带 ����ֱ�
                 * 
                 */
                break;
            case 4:
                /*����Ʈ â �г� �ѱ� ���� 
                 * 
                 *  �̰��� ����Ʈ â �г� UI�� Ű�� ����
                 *  �ڵ带 ����ֱ�
                 * 
                 */
                break;
            case 5:
                if (settingPanel.activeSelf) // �����г� ������Ʈ�� ���� ������
                {
                    settingPanel.SetActive(false); // �����г��� ����

                }
                else // �޴��ٰ� ����������
                {
                    settingPanel.SetActive(true); // �����г��� �ѱ�
                }
                break;
            case 6:
                MenuOpen();
                break;
            // �߰����� ��ư�� ���� ���̽��� �ʿ��ϴٸ� ���⿡ �߰�
            default:
                Debug.Log("Unknown button index");
                break;
        }

    }
    // ����ð��� ����ϴ� �ż���
    public void GetCurrentDate()
    {
        InfoDate.text = DateTime.Now.ToString("yyyy��MM��dd��");
        InfoTime.text = DateTime.Now.ToString("HH��mm��");

    }
    public void GetCurrentExp()
    {
        int currentGold = PlayerPrefs.GetInt(GameManager.goldCountKey); // ���� ��� �� ��������
        
        
        moneytext.text = "" + currentGold; // �Ӵ��ؽ�Ʈ�� �̴ϰ��ӿ��� ���� ��尪�� ����(hb)
        // PlayerPrefs���� ����ġ�� �ҷ��� UI�� ����
        int currentExp = PlayerPrefs.GetInt(GameManager.expCountKey);

        // ����ġ ȹ�淮�� 10000�� �Ѿ�� ������
        if (currentExp >= 10000)
        {
            currentExp -= 10000; // �������� �ʿ��� ����ġ�� �����ϰ� ������ �� ����
            /*
             * �������� ���� �ڵ� �Է�
             */
        }
        exptext.text = currentExp.ToString() + "/10000"; // �������ġ / ���������� �ʿ��� ����ġ

      
    }

    // ��� ���� �׽�Ʈ�� ��ư , ���������� 1000��� ȹ��
    public void GoldBug()
    {
        PlayerPrefs.SetInt(GameManager.goldCountKey, PlayerPrefs.GetInt(GameManager.goldCountKey) + 1000);
        PlayerPrefs.Save();
    }

    /*
       (hn) �� ��ȯ �Լ�
    */
    public void LoadScene()
    {
        // �̴ϰ��� ī��Ʈ�� 0 �̻��̶�� �� ��ȯ
        if(GameManager.minigameCount>0)
        {
            SceneManager.LoadScene(1); //�̴ϰ��� �� ���� �ڵ�(HB)
        }
        else // �̴� ���� ī��Ʈ�� 0 ���϶�� 
        {
            minigamePanel.GetComponentInChildren<Text>().text = "�̴ϰ��� ���� Ƚ����\r\n ��� �����߽��ϴ�";
        }
        
    }
}
