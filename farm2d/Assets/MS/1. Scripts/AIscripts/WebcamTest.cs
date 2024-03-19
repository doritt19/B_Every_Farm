using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebcamTest : MonoBehaviour
{
    private WebCamTexture webCamTexture;

    // Start is called before the first frame update
    void Start()
    {
        // 사용 가능한 웹캠 디바이스 찾기
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length == 0)
        {
            Debug.Log("웹캠을 찾을 수 없습니다.");
            return;
        }

        // 첫 번째 웹캠 선택
        WebCamDevice selectedDevice = devices[1];
        webCamTexture = new WebCamTexture(selectedDevice.name);

        // 웹캠 텍스쳐를 Renderer에 할당하여 웹캠 화면 표시
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = webCamTexture;

        // 웹캠 시작
        webCamTexture.Play();
    }

    private void OnDestroy()
    {
        // 웹캠 리소스 해제
        if (webCamTexture != null)
        {
            webCamTexture.Stop();
            webCamTexture = null;
        }
    }
}
