//using JetBrains.Annotations;
//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;


//public class GameManager1 : MonoBehaviour
//{
//    public Image goldImage; // ��ȭUI�̹���
//    public Image[] hpImage; // ü��UI�̹���
//    public Button soundButton; // �����̹�����ư
//    public Button restartButton; //���� ����� ��ư
//    public AudioSource audioSource; // ���� ����ҽ�
//    public Text goldText; // ��ȭ����ǥ��
//    public Slider timerSlider; // �ð�UI�����̴�
//    public Image timerImage; // �ð�UI�̹���
//    public Button quitButton; // ���� ���� ��ư
//    public Text goldCountText; // �����̳����� ǥ�õ� �� ���� ���.

//    // PlayerPrefs Ű
//    public const string GoldCountKey = "GoldCount";




//    public bool isPlaying = false; // ��������������� ���۵ɶ� ��������.
//    private int playerHealth = 3; // �÷��̾� ü��
//    public bool isrestart = false; // ����ŸƮ��ư�� ���۽ÿ��� ��������


//    public static GameManager instance; // ���ӸŴ��� �ν��Ͻ�ȭ

//    public int goldCount; // ��ȭ����ī��Ʈ
//    public float currentTime = 30f; // ��������ð��ʱ⼳��
    

//    private void Awake()
//    {
//        if (instance == null)
//        {
//            instance = this;
//        }
//    }
       
       
           
       
  
//    void Start()
//    {
        
//        goldText.text = goldCount.ToString();
//        // playButton�� Ŭ�� �̺�Ʈ �ڵ鷯 �߰�
//        //soundButton.onClick.AddListener(ToggleAudio);
//        playerHealth = 3;


//    }

//    // Update is called once per frame
//    void Update()
//    {
//        Timing();
        



//    }
//    public void Timing()
//    {

//        // �ð��� ���ҽ�ŵ�ϴ�.
//        currentTime -= Time.deltaTime;

//        // Ÿ�̸� �����̴� ������Ʈ
//        timerSlider.value = currentTime / 30f;
//        if (currentTime <= 0)
//        {
//            DecreaseHealth();
//            currentTime = 30;
//            Time.timeScale = 0f; // ������ ����ϴ�.
//            Debug.Log("���-1");
//            restartButton.gameObject.SetActive(true);
//            quitButton.gameObject.SetActive(true);
//            goldCountText.text = "Gold Count" + goldCount.ToString();
//            goldCountText.gameObject.SetActive(true);


//            // ���⿡ ���� ���ῡ ���� ó���� �߰��� �� �ֽ��ϴ�.

//        }
        
//    }
    
//    public void ToggleAudio()
//    {
//        // ����� ���� ����
//        if (isPlaying)
//        {
//            audioSource.Pause(); // ��� ���̸� �Ͻ� ����
//            Debug.Log("��������");
//        }
//        else
//        {
//            audioSource.Play(); // ��� ���� �ƴϸ� ���
//            Debug.Log("�������");
//        }

//        // ����� ���� ������Ʈ
//        isPlaying = !isPlaying;

//    }
//    public void DecreaseHealth()
//    {
//        // �÷��̾� ü�� ����
//        playerHealth--;

//        // ü�� UI ����
//        if (playerHealth >= 0 && playerHealth < hpImage.Length)
//        {
//            hpImage[playerHealth].gameObject.SetActive(false); // �ش� ü�� �̹����� ��Ȱ��ȭ
//        }

//        // �÷��̾� ü���� ��� �����Ǹ� ���� ����
//        if (playerHealth <= 0)
//        {
//            EndGame();
//        }


//    }
//    void EndGame()
//    {
//        // ���� ���� ó��
//        Debug.Log("ü���� ��� �����Ǿ� ������ ����˴ϴ�.");
//        // ���� ���� UI�� Ȱ��ȭ�ϰų� �ٸ� ���� ó���� ������ �� �ֽ��ϴ�.
       
        
           

//        // ���� ����
//        //Application.Quit();
//    }
//   public void reStart()
//    {
//        restartButton.gameObject.SetActive(false);
//        Time.timeScale = 1f; // ������ �����մϴ�.
//        quitButton.gameObject.SetActive(false);
       
//        goldCountText.gameObject.SetActive(false);

//    }
//    public void Exitgame()
//    {

//        // goldCount ����
//        SaveGoldCount();
//        SceneManager.LoadScene(0);
//    }
//    // ���� ���� �� goldCount�� PlayerPrefs�� ����
//    private void SaveGoldCount()
//    {
//        PlayerPrefs.SetInt(GoldCountKey, goldCount);
//    }
//    // �ε� �� goldCount�� PlayerPrefs���� �ҷ�����
//    private void LoadGoldCount()
//    {
//        if (PlayerPrefs.HasKey(GoldCountKey))
//        {
//            goldCount = PlayerPrefs.GetInt(GoldCountKey);
//        }
//    }



//}
