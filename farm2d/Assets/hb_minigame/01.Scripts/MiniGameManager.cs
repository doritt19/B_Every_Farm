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

    // �ѳ� ����, ���� ����� �г�
    public GameObject restartPanel;  // ���� �����, ����, ���� ��带 ǥ���� �г�
    public Text goldCountText; // �����̳����� ǥ�õ� �� ���� ���.

    /* �ѳ� ����, ���� ������Ʈ�� �����鼭 �ѹ��� ���� 
    public Button restartButton; //���� ����� ��ư
    public Button quitButton; // ���� ���� ��ư
    */


    public AudioSource audioSource; // ���� ����ҽ�
    public Text goldText; // ��ȭ����ǥ��
    public Slider timerSlider; // �ð�UI�����̴�
    public Image timerImage; // �ð�UI�̹���
 

    // PlayerPrefs Ű �ҷ��� ����
    private string minigameGold = "MinigameGold";
    
 
    // ���� �÷��̾��� ü��
    public int playerHealth; // �̴ϰ��� Ƚ��

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
            PlayerPrefs.SetInt(minigameGold, 0);
            

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
        playerHealth = GameManager.minigameCount;

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

            // �ѳ�����, ���� ����� ���� ���� �ȳ� �г� Ȱ��ȭ
            EndGame();


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

        restartPanel.SetActive(true);
        goldCountText.text = PlayerPrefs.GetInt("Goldcount") + " + ���� ��� :" + goldCount.ToString();


        // ���� ����
        //Application.Quit();
    }
    public void reStart()
    {
        if (playerHealth > 0) // �̴� ���� Ƚ���� ���Ҵٸ� ���� �����
        {
            restartPanel.SetActive(false);

            Time.timeScale = 1f; // ������ �����մϴ�.

        }
        else
        {
            Debug.Log("����ۺҰ�");
           
        }
       

        /* �ѳ� ���� �ѹ��� ����
        restartButton.gameObject.SetActive(false); // ����۹�ư ��Ȱ��ȭ
        
        quitButton.gameObject.SetActive(false); // ���� ������ ��ư ��Ȱ��ȭ

        goldCountText.gameObject.SetActive(false); // ���ī��Ʈ�ؽ�Ʈ ��Ȱ��ȭ
        */

    }

    public void Quitgame()
    {

        // goldCount ����
        SaveGoldCount();
        // playerHealth�� ����
        
        /* �ѳ� ���� ����ġ�� �̴ϰ��ӿ��� ȹ�� �Ұ�
        // ���� ����ġ�� ������
        int currentExp = PlayerPrefs.GetInt("Experience");
  
        // ���ο� ����ġ�� ����
        PlayerPrefs.SetInt("Experience", currentExp);
        */

        // ������� ���� ���� ��带 ���� �� ����
        int currentGold = PlayerPrefs.GetInt(GameManager.goldCountKey) + PlayerPrefs.GetInt(minigameGold);
        Debug.Log("�ѳ� �׽�Ʈ�� ���� ���" + currentGold);
        // ���ο� ��尪�� ����
        PlayerPrefs.SetInt(GameManager.goldCountKey, currentGold);

        // PlayerPrefs�� ��� ����
        PlayerPrefs.Save();


        // ���� ����� gamecount�� ����
        GameManager.minigameCount = playerHealth;



        SceneManager.LoadScene("Main");

    }
    // ���� ���� �� goldCount�� PlayerPrefs�� ����
    private void SaveGoldCount()
    {
        PlayerPrefs.SetInt(minigameGold, goldCount);
        //  MainUI mainUI = FindObjectOfType<MainUI>();
        // mainUI.moneytext.text = PlayerPrefs.GetString(GoldCountKey);
    }
    // �ε� �� goldCount�� PlayerPrefs���� �ҷ�����
    private void LoadGoldCount()
    {
        if (PlayerPrefs.HasKey(minigameGold))
        {
            goldCount = PlayerPrefs.GetInt(minigameGold);
        }
    }

    // ��� ���� �޼���
    public void AddGold(int amount)
    {
        goldCount += amount*10;
        goldText.text = goldCount.ToString();
       
        // �ѳ� ���� �̴ϰ��ӿ��� ����ġ ȹ��x
        // ����ġ ������Ʈ
        //UpdateExperience(amount);
    }

    // ����ġ ������Ʈ �޼��� 
    // �ѳ� ���� �̴ϰ��ӿ��� ����ġ ȹ��x ������� �ʴ� �Լ�
    public void UpdateExperience(int goldEarned)
    {
        // ����ġ�� ȹ���� ����� ���� ������ ������ �� �ֽ��ϴ�.
        int experienceEarned = goldEarned / 10; // ����� 10%�� ����ġ�� ���� ����

        // ���� ����ġ�� ������
        int currentExp = PlayerPrefs.GetInt(GameManager.expCountKey, 0);
        currentExp += experienceEarned;

        // ���ο� ����ġ�� ����
        PlayerPrefs.SetInt(GameManager.expCountKey, currentExp);

        // PlayerPrefs�� ��� ����
        PlayerPrefs.Save();
    }




}
