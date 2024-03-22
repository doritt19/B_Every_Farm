using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // PlayerPrefs 키
    public const string goldCountKey = "GoldCount";
    public const string expCountKey = "ExpCount"; // 획득한 경험치
    public const string SeedCountKey = "12";
    public static int minigameCount = 3; // 미니게임 횟수
    Text text; 
    public static bool weather; // 물주기 스킬 비오는 날 체크용

    // 싱글톤 패턴을 사용하기 위한 인스턴스 변수
    private static GameManager _instance;
    // 인스턴스에 접근하기 위한 프로퍼티
    public static GameManager Instance
    {
        get
        {
            // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }
 
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        // 아래의 함수를 사용하여 씬이 전환되더라도 선언되었던 인스턴스가 파괴되지 않는다.
        DontDestroyOnLoad(gameObject);

        //TestGame(); // 테스트를 위한 함수
        weather = false;
    }
    private void Update()
    {
        
    }



    /// <summary>
    /// 테스트를 위한 함수
    /// </summary>
    public void TestGame()
    {
        
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt(goldCountKey, 20000);
        PlayerPrefs.SetInt(expCountKey, 0);
        PlayerPrefs.SetInt("plantNum", 0);
        PlayerPrefs.Save();
    }
    // 게임 종료 버튼을 클릭할 때 호출될 메서드
    public void QuitGame()
    {
        // 게임 종료
        Application.Quit();
    }
}
