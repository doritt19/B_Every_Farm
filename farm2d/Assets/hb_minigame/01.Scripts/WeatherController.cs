using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WeatherController : MonoBehaviour
{
    public string weatherAPIURL = "https://api.openweathermap.org/data/2.5/weather?q=Seoul&appid=5a5e0631966067edae99f742f5056ee8&units=metric";
    //public Text weatherText;
    //public Button weatherButton;
    public Image weatherImage;
    public Sprite cloudySprite;
    public Sprite sunnySprite;
    public string weatherName;


    void Start()
    {
        // ó���� �� ���� ���� ȣ���Ͽ� ������Ʈ�� �����մϴ�.
        UpdateWeather();

        // 6�ð����� ������Ʈ�� �ݺ��մϴ�.
        InvokeRepeating("UpdateWeather", 0, 21600); // 6�ð��� 6 * 60 * 60 = 21600�� �Դϴ�.
    }
    IEnumerator FetchWeather()
    {
        string cityName = "Seoul";
        string url = weatherAPIURL.Replace("Seoul", cityName).Replace("5a5e0631966067edae99f742f5056ee8", "5a5e0631966067edae99f742f5056ee8");

        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            string jsonResponse = request.downloadHandler.text;
            JObject weatherData = JObject.Parse(jsonResponse);
            string description = weatherData["weather"][0]["description"].ToString();
            float temperature = float.Parse(weatherData["main"]["temp"].ToString());
            Debug.Log(description);
            weatherName = description;
            //weatherText.text = "����: " + description + "\n�µ�: " + temperature + "��C";
            SetWeatherImage(description);
            Debug.Log("GetWeather ����");
        }
    }
    public void UpdateWeather()
    {
        StartCoroutine(FetchWeather());
        
        Debug.Log("���� ������Ʈ ����");
    }
    public void Update()
    {
        
    }
    // API�κ��� ���� ���� ������ �������� �̹����� �����ϴ� �Լ�
    public void SetWeatherImage(string weatherInfo)
    {

        // �帲�� ���
        if (weatherName == "clouds")
        {
            weatherImage.sprite = cloudySprite; // �帲 �̹����� ����
            GameManager.weather = true;
        }
        // ������ ���
        else
        {
            weatherImage.sprite = sunnySprite; // ���� �̹����� ����
        }

    }
    // API�κ��� ���� ���� �������� �ֿ� ���� ���¸� �����ϴ� �Լ�
  

}
