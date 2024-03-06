using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MiniGameManager : MonoBehaviour
{
    public Image goldImage; // ��ȭUI�̹���
    public Image[] hpImage; // ü��UI�̹���
    public Button soundButton; // �����̹�����ư
    public Button restartButton; //���� ����� ��ư
    public AudioSource audioSource; // ���� ����ҽ�
    public Text goldText; // ��ȭ����ǥ��
    public Slider timerSlider; // �ð�UI�����̴�
    public Image timerImage; // �ð�UI�̹���
    public Button quitButton; // ���� ���� ��ư
    public Text goldCountText; // �����̳����� ǥ�õ� �� ���� ���.

    // PlayerPrefs Ű
    public const string GoldCountKey = "GoldCount";
    
    // �÷��̾��� �ִ� ü��
    public const int MaxPlayerHealth = 3;
    // ���� �÷��̾��� ü��
    public  int playerHealth = 3; // �÷��̾� ü��

    public bool isPlaying = false; // ��������������� ���۵ɶ� ��������.

    public bool isrestart = false; // ����ŸƮ��ư�� ���۽ÿ��� ��������


    public static MiniGameManager instance; // �̴ϰ��ӸŴ��� �ν��Ͻ�ȭ


    public int goldCount; // ��ȭ����ī��Ʈ
    public float currentTime = 30f; // ��������ð��ʱ⼳��


    private void Awake()
    {
        
        Time.timeScale = 1f;

        if (instance == null)
        {
            instance = this;
           
            Debug.Log("�ν��Ͻ������.");
            //DontDestroyOnLoad(gameObject);
            PlayerPrefs.SetInt(GoldCountKey, 0);
            

        }
        else if (instance != this)
        {
            // �̹� �ν��Ͻ��� �����ϸ� ���� �ν��Ͻ��� �ı��Ͽ� ���ο� �ν��Ͻ��� �������� �ʵ��� ��
            Destroy(gameObject);

        }

        //else
        //{
        //    playerHealth = MaxPlayerHealth; // �⺻������ �ִ� ü�� ����
        //}
    }

    



    void Start()
    {

        goldText.text = goldCount.ToString(); // ���ǥ�� ���ī�������� ����ȭ
        // playButton�� Ŭ�� �̺�Ʈ �ڵ鷯 �߰�
        soundButton.onClick.AddListener(ToggleAudio);



    }

    // Update is called once per frame
    void Update()
    {
        Timing();




    }
    public void Timing()
    {
        DecreaseHealth();
        // �ð��� ���ҽ�ŵ�ϴ�.
        currentTime -= Time.deltaTime;

        // Ÿ�̸� �����̴� ������Ʈ
        timerSlider.value = currentTime / 30f;
        if (currentTime <= 0)
        {
            playerHealth--;
         
            currentTime = 30;
            Time.timeScale = 0f; // ������ ����ϴ�.
            Debug.Log("���-1");
            restartButton.gameObject.SetActive(true);
            quitButton.gameObject.SetActive(true);
            goldCountText.text = "Gold Count" + goldCount.ToString();
            goldCountText.gameObject.SetActive(true);


            // ���⿡ ���� ���ῡ ���� ó���� �߰��� �� �ֽ��ϴ�.

        }

    }

    public void ToggleAudio()
    {
        // ����� ���� ����
        if (isPlaying)
        {
            audioSource.Pause(); // ��� ���̸� �Ͻ� ����
            Debug.Log("��������");
        }
        else
        {
            audioSource.Play(); // ��� ���� �ƴϸ� ���
            Debug.Log("�������");
        }

        // ����� ���� ������Ʈ
        isPlaying = !isPlaying;

    }
    public void DecreaseHealth()
    {
        // �÷��̾� ü�� ����
      

        // ü�� UI ����
        if (playerHealth >= 0 && playerHealth < hpImage.Length)
        {
            hpImage[playerHealth].gameObject.SetActive(false); // �ش� ü�� �̹����� ��Ȱ��ȭ
        }

        // �÷��̾� ü���� ��� �����Ǹ� ���� ����
        if (playerHealth <= 0)
        {
            EndGame();
        }


    }
    void EndGame()
    {
        // ���� ���� ó��
        Debug.Log("ü���� ��� �����Ǿ� ������ ����˴ϴ�.");
        
       
        // ���� ���� UI�� Ȱ��ȭ�ϰų� �ٸ� ���� ó���� ������ �� �ֽ��ϴ�.




        // ���� ����
        //Application.Quit();
    }
    public void reStart()
    {
        restartButton.gameObject.SetActive(false); // ����۹�ư ��Ȱ��ȭ
        Time.timeScale = 1f; // ������ �����մϴ�.
        quitButton.gameObject.SetActive(false); // ���� ������ ��ư ��Ȱ��ȭ

        goldCountText.gameObject.SetActive(false); // ���ī��Ʈ�ؽ�Ʈ ��Ȱ��ȭ


    }

    public void Quitgame()
    {

        // goldCount ����
        SaveGoldCount();
        // playerHealth�� ����
        // ����ġ ȹ��
        int expGained = 300;

        // ���� ����ġ�� ������
        int currentExp = PlayerPrefs.GetInt("Experience", 0);
        currentExp += expGained;

        // ���ο� ����ġ�� ����
        PlayerPrefs.SetInt("Experience", currentExp);

        // PlayerPrefs�� ��� ����
        PlayerPrefs.Save();
            



        SceneManager.LoadScene(0);

    }
    // ���� ���� �� goldCount�� PlayerPrefs�� ����
    private void SaveGoldCount()
    {
        PlayerPrefs.SetInt(GoldCountKey, goldCount);
        //  MainUI mainUI = FindObjectOfType<MainUI>();
        // mainUI.moneytext.text = PlayerPrefs.GetString(GoldCountKey);
    }
    // �ε� �� goldCount�� PlayerPrefs���� �ҷ�����
    private void LoadGoldCount()
    {
        if (PlayerPrefs.HasKey(GoldCountKey))
        {
            goldCount = PlayerPrefs.GetInt(GoldCountKey);
        }
    }

    // ��� ���� �޼���
    public void AddGold(int amount)
    {
        goldCount += amount;
        goldText.text = goldCount.ToString();
        // ����ġ ������Ʈ
        UpdateExperience(amount);
    }

    // ����ġ ������Ʈ �޼���
    public  void UpdateExperience(int goldEarned)
    {
        // ����ġ�� ȹ���� ����� ���� ������ ������ �� �ֽ��ϴ�.
        int experienceEarned = goldEarned / 10; // ����� 10%�� ����ġ�� ���� ����

        // ���� ����ġ�� ������
        int currentExp = PlayerPrefs.GetInt("Experience", 0);
        currentExp += experienceEarned;

        // ���ο� ����ġ�� ����
        PlayerPrefs.SetInt("Experience", currentExp);

        // PlayerPrefs�� ��� ����
        PlayerPrefs.Save();
    }




}
