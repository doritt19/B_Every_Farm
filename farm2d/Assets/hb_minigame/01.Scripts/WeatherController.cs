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
        // 처음에 한 번은 직접 호출하여 업데이트를 시작합니다.
        UpdateWeather();

        // 6시간마다 업데이트를 반복합니다.
        InvokeRepeating("UpdateWeather", 0, 21600); // 6시간은 6 * 60 * 60 = 21600초 입니다.
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
            //weatherText.text = "날씨: " + description + "\n온도: " + temperature + "°C";
            SetWeatherImage(description);
            Debug.Log("GetWeather 실행");
        }
    }
    public void UpdateWeather()
    {
        StartCoroutine(FetchWeather());
        
        Debug.Log("날씨 업데이트 성공");
    }
    public void Update()
    {
        
    }
    // API로부터 받은 날씨 정보를 기준으로 이미지를 설정하는 함수
    public void SetWeatherImage(string weatherInfo)
    {

        // 흐림인 경우
        if (weatherName == "clouds")
        {
            weatherImage.sprite = cloudySprite; // 흐림 이미지로 설정
            GameManager.weather = true;
        }
        // 맑음인 경우
        else
        {
            weatherImage.sprite = sunnySprite; // 맑음 이미지로 설정
        }

    }
    // API로부터 받은 날씨 정보에서 주요 날씨 상태를 추출하는 함수
  

}
