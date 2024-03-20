using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��ķ���κ��� ������ �����ͼ� ����Ƽ�� ǥ�����ִ� �Լ�
/// </summary>
public class CameraView : MonoBehaviour
{
    RawImage rawImage;
    WebCamTexture webCamTexture;
    // UI ����� ��Ⱦ�� �����ϰ� ���ִ� ������Ʈ
    AspectRatioFitter aspectRatioFitter;
    // ��Ⱦ�� ���� �Ǿ����� ���θ� �����ϴ� �÷���
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
            ratioSet = true; // ��Ⱦ�� ���� �Ǿ��ٰ� ����
            SetAspectRatio();

        }
    }
    /// <summary>
    /// ��Ⱦ�� �������ִ� �Լ�
    /// </summary>
    void SetAspectRatio()
    {
        aspectRatioFitter.aspectRatio = (float)webCamTexture.width / (float)webCamTexture.height;
        // WebCamTexture�� �ʺ� ���̷� ���� ���� fitter�� ��Ⱦ��� ����.
    }

    /// <summary>
    /// ��ķ ����
    /// </summary>
    void initWebCam()
    {
        string camName = WebCamTexture.devices[0].name;
        // ù ��° ��ķ�� ��������,
        // ��ķ�� �������ִ� ������ width, height ������. ������ 30.
        webCamTexture = new WebCamTexture(camName, Screen.width, Screen.height, 30);
        rawImage.texture = webCamTexture;
        if (Application.platform == RuntimePlatform.Android)
        {
            rawImage.transform.localScale = new Vector3(1f, -1f, 1f); // �̹����� �������� ������
        }
        webCamTexture.Play();
    }

    /// <summary>
    /// ��ķ �ؽ�ó�� ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <returns></returns>
    public WebCamTexture GetCamImage()
    {
        return webCamTexture; // ���� ��ķ �ؽ�ó�� ������ ��ȯ
    }
}
