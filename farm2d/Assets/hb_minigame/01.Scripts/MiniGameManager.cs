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

    // 한나 수정, 게임 재시작 패널
    public GameObject restartPanel;  // 게임 재시작, 종료, 얻은 골드를 표시할 패널
    public Text goldCountText; // 게임이끝날떄 표시될 총 얻은 골드.

    /* 한나 수정, 게임 오브젝트로 담으면서 한번에 관리 
    public Button restartButton; //게임 재시작 버튼
    public Button quitButton; // 게임 종료 버튼
    */


    public AudioSource audioSource; // 게임 사운드소스
    public Text goldText; // 재화점수표시
    public Slider timerSlider; // 시간UI슬라이더
    public Image timerImage; // 시간UI이미지
 

    // PlayerPrefs 키 불러올 공간
    private string minigameGold = "MinigameGold";
    
 
    // 현재 플레이어의 체력
    public int playerHealth; // 미니게임 횟수

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
            PlayerPrefs.SetInt(minigameGold, 0);
            

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
        playerHealth = GameManager.minigameCount;

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

            // 한나수정, 게임 종료시 게임 종료 안내 패널 활성화
            EndGame();


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

        restartPanel.SetActive(true);
        goldCountText.text = PlayerPrefs.GetInt("Goldcount") + " + 얻은 골드 :" + goldCount.ToString();


        // 게임 종료
        //Application.Quit();
    }
    public void reStart()
    {
        if (playerHealth > 0) // 미니 게임 횟수가 남았다면 게임 재시작
        {
            restartPanel.SetActive(false);

            Time.timeScale = 1f; // 게임을 실행합니다.

        }
        else
        {
            Debug.Log("재시작불가");
           
        }
       

        /* 한나 수정 한번에 관리
        restartButton.gameObject.SetActive(false); // 재시작버튼 비활성화
        
        quitButton.gameObject.SetActive(false); // 게임 나가기 버튼 비활성화

        goldCountText.gameObject.SetActive(false); // 골드카운트텍스트 비활성화
        */

    }

    public void Quitgame()
    {

        // goldCount 저장
        SaveGoldCount();
        // playerHealth값 저장
        
        /* 한나 수정 경험치는 미니게임에서 획득 불가
        // 현재 경험치를 가져옴
        int currentExp = PlayerPrefs.GetInt("Experience");
  
        // 새로운 경험치를 저장
        PlayerPrefs.SetInt("Experience", currentExp);
        */

        // 벌어들인 골드와 현재 골드를 더한 값 저장
        int currentGold = PlayerPrefs.GetInt(GameManager.goldCountKey) + PlayerPrefs.GetInt(minigameGold);
        Debug.Log("한나 테스트용 현재 골드" + currentGold);
        // 새로운 골드값을 저장
        PlayerPrefs.SetInt(GameManager.goldCountKey, currentGold);

        // PlayerPrefs를 즉시 저장
        PlayerPrefs.Save();


        // 게임 종료시 gamecount를 저장
        GameManager.minigameCount = playerHealth;



        SceneManager.LoadScene("Main");

    }
    // 게임 종료 시 goldCount를 PlayerPrefs에 저장
    private void SaveGoldCount()
    {
        PlayerPrefs.SetInt(minigameGold, goldCount);
        //  MainUI mainUI = FindObjectOfType<MainUI>();
        // mainUI.moneytext.text = PlayerPrefs.GetString(GoldCountKey);
    }
    // 로드 시 goldCount를 PlayerPrefs에서 불러오기
    private void LoadGoldCount()
    {
        if (PlayerPrefs.HasKey(minigameGold))
        {
            goldCount = PlayerPrefs.GetInt(minigameGold);
        }
    }

    // 골드 증가 메서드
    public void AddGold(int amount)
    {
        goldCount += amount*10;
        goldText.text = goldCount.ToString();
       
        // 한나 수정 미니게임에선 경험치 획득x
        // 경험치 업데이트
        //UpdateExperience(amount);
    }

    // 경험치 업데이트 메서드 
    // 한나 수정 미니게임에선 경험치 획득x 사용하지 않는 함수
    public void UpdateExperience(int goldEarned)
    {
        // 경험치를 획득한 골드의 일정 비율로 설정할 수 있습니다.
        int experienceEarned = goldEarned / 10; // 골드의 10%를 경험치로 설정 예시

        // 현재 경험치를 가져옴
        int currentExp = PlayerPrefs.GetInt(GameManager.expCountKey, 0);
        currentExp += experienceEarned;

        // 새로운 경험치를 저장
        PlayerPrefs.SetInt(GameManager.expCountKey, currentExp);

        // PlayerPrefs를 즉시 저장
        PlayerPrefs.Save();
    }




}
