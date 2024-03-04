//using JetBrains.Annotations;
//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;


//public class GameManager1 : MonoBehaviour
//{
//    public Image goldImage; // 재화UI이미지
//    public Image[] hpImage; // 체력UI이미지
//    public Button soundButton; // 사운드이미지버튼
//    public Button restartButton; //게임 재시작 버튼
//    public AudioSource audioSource; // 게임 사운드소스
//    public Text goldText; // 재화점수표시
//    public Slider timerSlider; // 시간UI슬라이더
//    public Image timerImage; // 시간UI이미지
//    public Button quitButton; // 게임 종료 버튼
//    public Text goldCountText; // 게임이끝날떄 표시될 총 얻은 골드.

//    // PlayerPrefs 키
//    public const string GoldCountKey = "GoldCount";




//    public bool isPlaying = false; // 음악이재생중인지 시작될땐 꺼져있음.
//    private int playerHealth = 3; // 플레이어 체력
//    public bool isrestart = false; // 리스타트버튼이 시작시에는 꺼져있음


//    public static GameManager instance; // 게임매니저 인스턴스화

//    public int goldCount; // 재화점수카운트
//    public float currentTime = 30f; // 게임진행시간초기설정
    

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
//        // playButton에 클릭 이벤트 핸들러 추가
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

//        // 시간을 감소시킵니다.
//        currentTime -= Time.deltaTime;

//        // 타이머 슬라이더 업데이트
//        timerSlider.value = currentTime / 30f;
//        if (currentTime <= 0)
//        {
//            DecreaseHealth();
//            currentTime = 30;
//            Time.timeScale = 0f; // 게임을 멈춥니다.
//            Debug.Log("목숨-1");
//            restartButton.gameObject.SetActive(true);
//            quitButton.gameObject.SetActive(true);
//            goldCountText.text = "Gold Count" + goldCount.ToString();
//            goldCountText.gameObject.SetActive(true);


//            // 여기에 게임 종료에 대한 처리를 추가할 수 있습니다.

//        }
        
//    }
    
//    public void ToggleAudio()
//    {
//        // 오디오 상태 변경
//        if (isPlaying)
//        {
//            audioSource.Pause(); // 재생 중이면 일시 정지
//            Debug.Log("음악정지");
//        }
//        else
//        {
//            audioSource.Play(); // 재생 중이 아니면 재생
//            Debug.Log("음악재생");
//        }

//        // 오디오 상태 업데이트
//        isPlaying = !isPlaying;

//    }
//    public void DecreaseHealth()
//    {
//        // 플레이어 체력 감소
//        playerHealth--;

//        // 체력 UI 갱신
//        if (playerHealth >= 0 && playerHealth < hpImage.Length)
//        {
//            hpImage[playerHealth].gameObject.SetActive(false); // 해당 체력 이미지를 비활성화
//        }

//        // 플레이어 체력이 모두 소진되면 게임 종료
//        if (playerHealth <= 0)
//        {
//            EndGame();
//        }


//    }
//    void EndGame()
//    {
//        // 게임 종료 처리
//        Debug.Log("체력이 모두 소진되어 게임이 종료됩니다.");
//        // 게임 종료 UI를 활성화하거나 다른 종료 처리를 수행할 수 있습니다.
       
        
           

//        // 게임 종료
//        //Application.Quit();
//    }
//   public void reStart()
//    {
//        restartButton.gameObject.SetActive(false);
//        Time.timeScale = 1f; // 게임을 실행합니다.
//        quitButton.gameObject.SetActive(false);
       
//        goldCountText.gameObject.SetActive(false);

//    }
//    public void Exitgame()
//    {

//        // goldCount 저장
//        SaveGoldCount();
//        SceneManager.LoadScene(0);
//    }
//    // 게임 종료 시 goldCount를 PlayerPrefs에 저장
//    private void SaveGoldCount()
//    {
//        PlayerPrefs.SetInt(GoldCountKey, goldCount);
//    }
//    // 로드 시 goldCount를 PlayerPrefs에서 불러오기
//    private void LoadGoldCount()
//    {
//        if (PlayerPrefs.HasKey(GoldCountKey))
//        {
//            goldCount = PlayerPrefs.GetInt(GoldCountKey);
//        }
//    }



//}
