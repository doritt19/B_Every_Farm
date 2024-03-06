using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MiniGameManager : MonoBehaviour
{
    public Image goldImage; // 재화UI이미지
    public Image[] hpImage; // 체력UI이미지
    public Button soundButton; // 사운드이미지버튼
    public Button restartButton; //게임 재시작 버튼
    public AudioSource audioSource; // 게임 사운드소스
    public Text goldText; // 재화점수표시
    public Slider timerSlider; // 시간UI슬라이더
    public Image timerImage; // 시간UI이미지
    public Button quitButton; // 게임 종료 버튼
    public Text goldCountText; // 게임이끝날떄 표시될 총 얻은 골드.

    // PlayerPrefs 키
    public const string GoldCountKey = "GoldCount";
    
    // 플레이어의 최대 체력
    public const int MaxPlayerHealth = 3;
    // 현재 플레이어의 체력
    public  int playerHealth = 3; // 플레이어 체력

    public bool isPlaying = false; // 음악이재생중인지 시작될땐 꺼져있음.

    public bool isrestart = false; // 리스타트버튼이 시작시에는 꺼져있음


    public static MiniGameManager instance; // 미니게임매니저 인스턴스화


    public int goldCount; // 재화점수카운트
    public float currentTime = 30f; // 게임진행시간초기설정


    private void Awake()
    {
        
        Time.timeScale = 1f;

        if (instance == null)
        {
            instance = this;
           
            Debug.Log("인스턴스적용됌.");
            //DontDestroyOnLoad(gameObject);
            PlayerPrefs.SetInt(GoldCountKey, 0);
            

        }
        else if (instance != this)
        {
            // 이미 인스턴스가 존재하면 현재 인스턴스를 파괴하여 새로운 인스턴스가 생성되지 않도록 함
            Destroy(gameObject);

        }

        //else
        //{
        //    playerHealth = MaxPlayerHealth; // 기본값으로 최대 체력 설정
        //}
    }

    



    void Start()
    {

        goldText.text = goldCount.ToString(); // 골드표시 골드카운팅으로 문자화
        // playButton에 클릭 이벤트 핸들러 추가
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
        // 시간을 감소시킵니다.
        currentTime -= Time.deltaTime;

        // 타이머 슬라이더 업데이트
        timerSlider.value = currentTime / 30f;
        if (currentTime <= 0)
        {
            playerHealth--;
         
            currentTime = 30;
            Time.timeScale = 0f; // 게임을 멈춥니다.
            Debug.Log("목숨-1");
            restartButton.gameObject.SetActive(true);
            quitButton.gameObject.SetActive(true);
            goldCountText.text = "Gold Count" + goldCount.ToString();
            goldCountText.gameObject.SetActive(true);


            // 여기에 게임 종료에 대한 처리를 추가할 수 있습니다.

        }

    }

    public void ToggleAudio()
    {
        // 오디오 상태 변경
        if (isPlaying)
        {
            audioSource.Pause(); // 재생 중이면 일시 정지
            Debug.Log("음악정지");
        }
        else
        {
            audioSource.Play(); // 재생 중이 아니면 재생
            Debug.Log("음악재생");
        }

        // 오디오 상태 업데이트
        isPlaying = !isPlaying;

    }
    public void DecreaseHealth()
    {
        // 플레이어 체력 감소
      

        // 체력 UI 갱신
        if (playerHealth >= 0 && playerHealth < hpImage.Length)
        {
            hpImage[playerHealth].gameObject.SetActive(false); // 해당 체력 이미지를 비활성화
        }

        // 플레이어 체력이 모두 소진되면 게임 종료
        if (playerHealth <= 0)
        {
            EndGame();
        }


    }
    void EndGame()
    {
        // 게임 종료 처리
        Debug.Log("체력이 모두 소진되어 게임이 종료됩니다.");
        
       
        // 게임 종료 UI를 활성화하거나 다른 종료 처리를 수행할 수 있습니다.




        // 게임 종료
        //Application.Quit();
    }
    public void reStart()
    {
        restartButton.gameObject.SetActive(false); // 재시작버튼 비활성화
        Time.timeScale = 1f; // 게임을 실행합니다.
        quitButton.gameObject.SetActive(false); // 게임 나가기 버튼 비활성화

        goldCountText.gameObject.SetActive(false); // 골드카운트텍스트 비활성화


    }

    public void Quitgame()
    {

        // goldCount 저장
        SaveGoldCount();
        // playerHealth값 저장
        // 경험치 획득
        int expGained = 300;

        // 현재 경험치를 가져옴
        int currentExp = PlayerPrefs.GetInt("Experience", 0);
        currentExp += expGained;

        // 새로운 경험치를 저장
        PlayerPrefs.SetInt("Experience", currentExp);

        // PlayerPrefs를 즉시 저장
        PlayerPrefs.Save();
            



        SceneManager.LoadScene(0);

    }
    // 게임 종료 시 goldCount를 PlayerPrefs에 저장
    private void SaveGoldCount()
    {
        PlayerPrefs.SetInt(GoldCountKey, goldCount);
        //  MainUI mainUI = FindObjectOfType<MainUI>();
        // mainUI.moneytext.text = PlayerPrefs.GetString(GoldCountKey);
    }
    // 로드 시 goldCount를 PlayerPrefs에서 불러오기
    private void LoadGoldCount()
    {
        if (PlayerPrefs.HasKey(GoldCountKey))
        {
            goldCount = PlayerPrefs.GetInt(GoldCountKey);
        }
    }

    // 골드 증가 메서드
    public void AddGold(int amount)
    {
        goldCount += amount;
        goldText.text = goldCount.ToString();
        // 경험치 업데이트
        UpdateExperience(amount);
    }

    // 경험치 업데이트 메서드
    public  void UpdateExperience(int goldEarned)
    {
        // 경험치를 획득한 골드의 일정 비율로 설정할 수 있습니다.
        int experienceEarned = goldEarned / 10; // 골드의 10%를 경험치로 설정 예시

        // 현재 경험치를 가져옴
        int currentExp = PlayerPrefs.GetInt("Experience", 0);
        currentExp += experienceEarned;

        // 새로운 경험치를 저장
        PlayerPrefs.SetInt("Experience", currentExp);

        // PlayerPrefs를 즉시 저장
        PlayerPrefs.Save();
    }




}
