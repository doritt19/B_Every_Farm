using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // PlayerPrefs Ű
    public const string goldCountKey = "GoldCount";
    public const string expCountKey = "ExpCount"; // ȹ���� ����ġ
    public static int minigameCount = 3; // �̴ϰ��� Ƚ��


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

        TestGame(); // �׽�Ʈ�� ���� �Լ�
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
    // Update is called once per frame
    void Update()
    {
        
    }
}
