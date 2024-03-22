using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // PlayerPrefs Ű
    public const string goldCountKey = "GoldCount";
    public const string expCountKey = "ExpCount"; // ȹ���� ����ġ
    public const string SeedCountKey = "12";
    public static int minigameCount = 3; // �̴ϰ��� Ƚ��
    Text text; 
    public static bool weather; // ���ֱ� ��ų ����� �� üũ��

    // �̱��� ������ ����ϱ� ���� �ν��Ͻ� ����
    private static GameManager _instance;
    // �ν��Ͻ��� �����ϱ� ���� ������Ƽ
    public static GameManager Instance
    {
        get
        {
            // �ν��Ͻ��� ���� ��쿡 �����Ϸ� �ϸ� �ν��Ͻ��� �Ҵ����ش�.
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
        // �ν��Ͻ��� �����ϴ� ��� ���λ���� �ν��Ͻ��� �����Ѵ�.
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        // �Ʒ��� �Լ��� ����Ͽ� ���� ��ȯ�Ǵ��� ����Ǿ��� �ν��Ͻ��� �ı����� �ʴ´�.
        DontDestroyOnLoad(gameObject);

        //TestGame(); // �׽�Ʈ�� ���� �Լ�
        weather = false;
    }
    private void Update()
    {
        
    }



    /// <summary>
    /// �׽�Ʈ�� ���� �Լ�
    /// </summary>
    public void TestGame()
    {
        
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt(goldCountKey, 20000);
        PlayerPrefs.SetInt(expCountKey, 0);
        PlayerPrefs.SetInt("plantNum", 0);
        PlayerPrefs.Save();
    }
    // ���� ���� ��ư�� Ŭ���� �� ȣ��� �޼���
    public void QuitGame()
    {
        // ���� ����
        Application.Quit();
    }
}
