using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebcamTest : MonoBehaviour
{
    private WebCamTexture webCamTexture;

    // Start is called before the first frame update
    void Start()
    {
        // ��� ������ ��ķ ����̽� ã��
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length == 0)
        {
            Debug.Log("��ķ�� ã�� �� �����ϴ�.");
            return;
        }

        // ù ��° ��ķ ����
        WebCamDevice selectedDevice = devices[1];
        webCamTexture = new WebCamTexture(selectedDevice.name);

        // ��ķ �ؽ��ĸ� Renderer�� �Ҵ��Ͽ� ��ķ ȭ�� ǥ��
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = webCamTexture;

        // ��ķ ����
        webCamTexture.Play();
    }

    private void OnDestroy()
    {
        // ��ķ ���ҽ� ����
        if (webCamTexture != null)
        {
            webCamTexture.Stop();
            webCamTexture = null;
        }
    }
}
