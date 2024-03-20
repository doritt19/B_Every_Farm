using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 웹캠으로부터 비디오를 가져와서 유니티에 표시해주는 함수
/// </summary>
public class CameraView : MonoBehaviour
{
    RawImage rawImage;
    WebCamTexture webCamTexture;
    // UI 요소의 종횡비를 유지하게 해주는 컴포넌트
    AspectRatioFitter aspectRatioFitter;
    // 종횡비가 설정 되었는지 여부를 추적하는 플래그
    bool ratioSet;

    void Start()
    {
        rawImage = GetComponent<RawImage>();
        aspectRatioFitter = GetComponent<AspectRatioFitter>();
        initWebCam();
    }

    // Update is called once per frame
    void Update()
    {
        if (webCamTexture.width > 100 && !ratioSet)
        {
            ratioSet = true; // 종횡비 설정 되었다고 변경
            SetAspectRatio();

        }
    }
    /// <summary>
    /// 종횡비를 설정해주는 함수
    /// </summary>
    void SetAspectRatio()
    {
        aspectRatioFitter.aspectRatio = (float)webCamTexture.width / (float)webCamTexture.height;
        // WebCamTexture의 너비를 높이로 나눈 값을 fitter의 종횡비로 설정.
    }

    /// <summary>
    /// 웹캠 실행
    /// </summary>
    void initWebCam()
    {
        string camName = WebCamTexture.devices[0].name;
        // 첫 번째 웹캠을 가져오고,
        // 웹캠이 제공해주는 비디오의 width, height 가져옴. 프레임 30.
        webCamTexture = new WebCamTexture(camName, Screen.width, Screen.height, 30);
        rawImage.texture = webCamTexture;
        if (Application.platform == RuntimePlatform.Android)
        {
            rawImage.transform.localScale = new Vector3(1f, -1f, 1f); // 이미지를 수직으로 뒤집음
        }
        webCamTexture.Play();
    }

    /// <summary>
    /// 웹캠 텍스처를 반환하는 함수
    /// </summary>
    /// <returns></returns>
    public WebCamTexture GetCamImage()
    {
        return webCamTexture; // 현재 웹캠 텍스처의 참조를 반환
    }
}
