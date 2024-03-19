using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

/// <summary>
/// ��ķ���� ĸó�� �̹����� ó���Ͽ� �ݹ� �Լ��� �����ϴ� �Լ�
/// </summary>
public class Preprocess : MonoBehaviour
{
    RenderTexture renderTexture; // �̹��� ó���� ���� �߰� ���� �ؽ�ó�� ����
    Vector2 scale = new Vector2(1, 1); // �̹��� ũ�� ������ ���� ������ ����
    Vector2 offest = Vector2.zero; // �̹��� �ڸ��⸦ ���� ������ ����

    UnityAction<byte[]> callback; // ó���� �̹��� �����͸� ���޹��� �ݹ� �Լ� ����

    /// <summary>
    /// ��ķ �ؽ�ó�� �޾� ũ�� ���� �� �ڸ��� �۾��� �����ϰ�, ����� �ݹ� �Լ��� �����ϴ� �޼���.
    /// </summary>
    public void ScaleAndCropImage(WebCamTexture webCamTexture, int desiredSize, UnityAction<byte[]> callback)
    {
        this.callback = callback; // ���޹��� �ݹ� �Լ��� ��� ������ ����

        if (renderTexture == null) // ���� �ؽ�ó�� �ʱ�ȭ ���� �ʾҴٸ�,
        {
            renderTexture = new RenderTexture(desiredSize, desiredSize, 0, RenderTextureFormat.ARGB32); // ������ ũ��� �������� �� ���� �ؽ�ó ����
        }

        scale.x = (float)webCamTexture.height / (float)webCamTexture.width; // ��ķ �ؽ�ó�� ��Ⱦ�� ��� �� ������ ���Ϳ� ����.
        offest.x = (1 - scale.x) / 2f; // ������ ������ ���� ������ �� ���
        Graphics.Blit(webCamTexture, renderTexture, scale, offest); // ��ķ �ؽ�ó�� ���� �ؽ�ó�� �����ϸ鼭 ũ�� ���� �� �������� ����
        AsyncGPUReadback.Request(renderTexture, 0, TextureFormat.RGB24, OnCompleteReadback); // ó���� �̹����� �񵿱� GPU �б⸦ ��û, �Ϸ�� ȣ��� �޼��带 ����
    }

    /// <summary>
    /// �񵿱� GPU �бⰡ �Ϸ�Ǿ��� �� ȣ��Ǵ� �޼���
    /// </summary>
    /// <param name="request"></param>
    void OnCompleteReadback(AsyncGPUReadbackRequest request)
    {
        if (request.hasError) // �б� ��û�� ������ ����ٸ�,
        {
            Debug.Log("GPU �б� ��û�� ������ �߻��߽��ϴ�."); // �α� ���
            return; // �޼��� ����
        }

        callback.Invoke(request.GetData<byte>().ToArray()); // ������ ���ٸ�, �о�� �����͸� ����Ʈ �迭�� ��ȯ�Ͽ� �ݹ� �Լ��� ����
    }
}
