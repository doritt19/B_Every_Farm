using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // UI 를 쓰기위해서 유니티 유아이 시스템 추가

public class MainUI : MonoBehaviour
{
    /*
     *  메인 UI 종류 4가지 및 부속 UI 3가지
     *  좌측 상단 : 프로필 정보 표시( 사용자 이름, 경험치, 돈)
     *  좌측 하단 : 가방과 스킬 버튼
     *      - 가방은 자식 UI로 부모 UI의 버튼 눌렀을 때 활성화
     *  우측 상단 : 날씨 날짜 시간 정보 
     *  우측 하단 : 메뉴바 
     *      - 메뉴 버튼을 눌러서 메뉴바 자식 UI를 활성화
     *          - 메뉴바의 각 버튼으로 각종 UI 활성화
     */

    public GameObject profilePanel; // 프로필 정보 패널
    public Text moneytext; // 프로필 머니 정보 텍스트 (hb)
    public Text exptext; // 프로필 경험치 정보 텍스트
    


    public GameObject inventoryPanel; // 스킬과 가방 버튼 패널


    public GameObject InfoPanel; // 날씨 정보 패널


    public GameObject menuPanel; // 메뉴 바
    public Button[] menuButtons; // 메뉴 바의 버튼을 담을 리스트
    public GameObject settingPanel; // 옵션 패널


    // Start is called before the first frame update
    private void Awake()
    {   
    }
    void Start()
    {
        int currentGold = int.Parse(moneytext.text); // 현재 텍스트에 있는 값 가져오기
        int goldFromMiniGame = PlayerPrefs.GetInt(MiniGameManager.GoldCountKey); // 미니게임에서 얻은 골드 값 가져오기
        int totalGold = currentGold + goldFromMiniGame; // 누적된 골드 값 계산
        moneytext.text = "" + totalGold; // 머니텍스트에 미니게임에서 얻은 골드값을 누적(hb)
        // PlayerPrefs에서 경험치를 불러와 UI에 적용
        int currentExp = PlayerPrefs.GetInt("Experience", 0);
        exptext.text = "Exp / " + currentExp.ToString();

        /*
         (hb)
      */


        menuPanel.SetActive(false);
        inventoryPanel.SetActive(false);
        settingPanel.SetActive(false);
        
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 메뉴바 활성화 버튼 매서드
    public void MenuOpen()
    {
        if (menuPanel != null) // 메뉴 패널 오브젝트가 할당되어있으면
        {
            if (menuPanel.activeSelf) // 메뉴바 오브젝트가 켜져 있으면
            {
                menuPanel.SetActive(false); // 메뉴바를 끄기

            }
            else // 메뉴바가 꺼져있으면
            {
                menuPanel.SetActive(true); // 메뉴바를 켜기
            }
        }
       
    }

    // 메뉴바의 버튼 클릭시 호출되는 매서드
    public void MenuButton(int buttonIndex) 
    {
        // 버튼의 인덱스에 따라 스위치 문으로 메서드 호출
        switch (buttonIndex)
        {
            case 0:
                SceneManager.LoadScene(1); //미니게임 씬 실행 코드(HB)
                /*
                 *  미니게임 씬 실행하는 코드 집어넣기
                 */
                break;
            case 1:

                if (inventoryPanel.activeSelf) // 세팅패널 오브젝트가 켜져 있으면
                {
                    inventoryPanel.SetActive(false); // 세팅패널을 끄기

                }
                else // 메뉴바가 꺼져있으면
                {
                    inventoryPanel.SetActive(true); // 세팅패널을 켜기
                }

                break;
            case 2:
                /*
                 * 오브젝트 위치를 편집기능을
                 * 수행하는 편집모드의
                 * 메서드 함수 집어넣기
                 */

                break;
            case 3:
                /* 상점 창 패널 켜기 끄기 
                 * 
                 *  이곳에 퀘스트 창 패널 UI를 키고 끄는
                 *  코드를 집어넣기
                 * 
                 */
                break;
            case 4:
                /*퀘스트 창 패널 켜기 끄기 
                 * 
                 *  이곳에 퀘스트 창 패널 UI를 키고 끄는
                 *  코드를 집어넣기
                 * 
                 */
                break;
            case 5:
                if (settingPanel.activeSelf) // 세팅패널 오브젝트가 켜져 있으면
                {
                    settingPanel.SetActive(false); // 세팅패널을 끄기

                }
                else // 메뉴바가 꺼져있으면
                {
                    settingPanel.SetActive(true); // 세팅패널을 켜기
                }
                break;
            case 6:
                MenuOpen();
                break;
            // 추가적인 버튼에 대한 케이스도 필요하다면 여기에 추가
            default:
                Debug.Log("Unknown button index");
                break;
        }

    }
    // 값을 받는 메서드 (HB)
   
}
